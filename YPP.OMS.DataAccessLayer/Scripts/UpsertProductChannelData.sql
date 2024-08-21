create or alter proc sp_UpsertProductData
as
begin
	--//Get latest data from MarketPlaceProduct
	select MarketPlaceProductId, max(id) as latestid
	into #latestproductdata
	from MarketPlaceProduct
	where IsPerform = 0 
	group by MarketPlaceProductId 
	
-- perform all new marketplace products 
	declare @maxId int
	set @maxId = (
		select top 1 latestid
		from #latestproductdata 
		order by latestid desc
	)
	update MarketPlaceProduct set IsPerform = 1 
	where Id <= @maxId and  IsPerform = 0
	
-- select necessary information from filtered marketplace products
	select  [mkpd].*
			, [mkp].Id AS 'MarketPlaceId'
			, [mkp].Type AS 'Type'
	into #sourceproductdata
	from MarketPlaceProduct [mkpd]
	inner join #latestproductdata b
		on [mkpd].MarketPlaceProductId = b.MarketPlaceProductId
		and [mkpd].Id = b.latestid
	left join Store [stor]
		on [mkpd].StoreId = [stor].Id
	left join MarketPlace [mkp]
		on [stor].MarketPlaceId = [mkp].Id

-- merge between marketplace product and product channel
	merge ProductChannel as [target]
	using #sourceproductdata as [source]
		on ([target].MarketPlaceProductId = [source].MarketPlaceProductId)
	when MATCHED and COALESCE([target].LastUpdatedAt, '1900-01-01') < COALESCE([source].UpdatedAt, '1900-01-01')
	then 
		update set [target].Metadata = [source].Metadata
				, [target].LastUpdatedAt = [source].UpdatedAt
				, [target].RunAt = GETDATE()
		
	when NOT MATCHED by target then 
		insert (Metadata, StoreId, MarketPlaceProductId, ChannelId, RunAt, MarketPlaceType) 
		values ([source].Metadata
				, [source].StoreId
				, [source].MarketPlaceProductId
				, [source].MarketPlaceId
				, GETDATE()
				, [source].Type);
end