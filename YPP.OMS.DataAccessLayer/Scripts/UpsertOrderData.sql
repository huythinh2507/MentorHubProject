create or alter proc sp_UpsertOrderData
as
begin
-- delete invalid marketplace orders
	delete
	from MarketPlaceOrder 
	where OrderedAt IS NULL
--//Get latest data from MarketPlaceOrder
	select MarketPlaceOrderNumber, max(id) as latestid
	into #latestdata
	from MarketPlaceOrder
	where IsPerform = 0 
	group by MarketPlaceOrderNumber 
	
-- perform all new marketplace orders 
	declare @maxId int
	set @maxId = (
		select top 1 latestid
		from #latestdata 
		order by latestid desc
	)
	update MarketPlaceOrder set IsPerform = 1 
	where Id <= @maxId and  IsPerform = 0
	
-- select necessary information from filtered marketplace orders
	select  [mkpo].*
			, [mkp].Id AS 'MarketPlaceId'
			, [mkp].Type AS 'Type'
	into #sourcedata
	from MarketPlaceOrder [mkpo]
	inner join #latestdata b
		on [mkpo].MarketPlaceOrderNumber = b.MarketPlaceOrderNumber
		and [mkpo].Id = b.latestid
	left join Store [stor]
		on [mkpo].StoreId = [stor].Id
	left join MarketPlace [mkp]
		on [stor].MarketPlaceId = [mkp].Id

-- merge between marketplace order and orderinfo 
	merge OrderInfo as [target]
	using #sourcedata as [source]
		on ([target].MarketPlaceOrderId = [source].MarketPlaceOrderNumber)
	when MATCHED and COALESCE([target].UpdatedAt, '1900-01-01') < COALESCE([source].UpdatedAt, '1900-01-01')
	then 
		update set [target].Metadata = [source].Metadata
				, [target].UpdatedAt = [source].UpdatedAt
				, [target].InsertedAt = GETDATE()
		
	when NOT MATCHED by target then 
		insert (Metadata, StoreId, MarketPlaceOrderId, ChannelId, InsertedAt, MarketPlaceType) 
		values ([source].Metadata
				, [source].StoreId
				, [source].MarketPlaceOrderNumber
				, [source].MarketPlaceId
				, GETDATE()
				, [source].Type);
end
