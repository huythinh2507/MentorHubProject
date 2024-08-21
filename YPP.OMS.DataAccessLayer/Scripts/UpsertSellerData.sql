create or alter proc sp_UpsertSellerData
as
begin
	--//Get latest data from MarketPlaceProduct
	select MarketPlaceSellerId, max(id) as latestid
	into #latestsellerdata
	from MarketPlaceSeller
	where IsPerform = 0 
	group by MarketplaceSellerId 
	
-- perform all new marketplace orders 
	declare @maxId int
	set @maxId = (
		select top 1 latestid
		from #latestsellerdata 
		order by latestid desc
	)
	update MarketPlaceSeller set IsPerform = 1 
	where Id <= @maxId and  IsPerform = 0
	
-- select necessary information from filtered marketplace orders
	select  [mkps].*
			, [mkp].Id AS 'MarketPlaceId'
			, [mkp].Type AS 'Type'
	into #sourcesellerdata
	from MarketPlaceSeller [mkps]
	inner join #latestsellerdata b
		on [mkps].MarketplaceSellerId = b.MarketPlaceSellerId
		and [mkps].Id = b.latestid
	left join Store [stor]
		on [mkps].StoreId = [stor].Id
	left join MarketPlace [mkp]
		on [stor].MarketPlaceId = [mkp].Id

-- merge between marketplace order and orderinfo 
	merge SellerInfoes as [target]
	using #sourcesellerdata as [source]
		on ([target].MarketPlaceSellerId = [source].MarketPlaceSellerId)
	when MATCHED and COALESCE([target].CreatedTime, '1900-01-01') < COALESCE([source].CreatedTime, '1900-01-01')
	then 
		update set [target].Metadata = [source].Metadata
				, [target].CreatedTime = [source].CreatedTime
				, [target].RunAt = GETDATE()
		
	when NOT MATCHED by target then 
		insert (Metadata, StoreId, MarketPlaceSellerId, RunAt, MarketPlaceType) 
		values ([source].Metadata
				, [source].StoreId
				, [source].MarketPlaceSellerId
				, GETDATE()
				, [source].Type);
end