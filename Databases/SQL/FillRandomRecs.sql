set nocount on

declare @userId bigint
declare @items table (num int identity primary key clustered, id int)
declare @n_recs int = 20, @userCounter int = 0
declare @items_count int

insert @items select id from Books order by id
select @items_count = @@ROWCOUNT

truncate table UserRecommendations

declare uids cursor local for select top 10 Id from users order by Id
open uids
fetch from uids into @userId
while @@FETCH_STATUS = 0
begin
	declare @recs table(itemid int primary key clustered, recnum int)
	delete @recs
	declare @count int = 0
	declare @itemNumber int, @itemId int
	while @count < @n_recs
	begin
		select @itemNumber = floor(rand() * @items_count) + 1
		select @itemId = id from @items where num = @itemNumber
		if not exists(select 1 from @recs where itemid = @itemId)
		begin
			select @count = @count + 1
			insert @recs select @itemId, @count
		end
	end

	insert UserRecommendations
	select @userCounter * @n_recs + recnum, @userId, itemid from @recs
	order by 1


	print @userid
	
	select @userCounter = @userCounter + 1

	fetch next from uids into @userId
end

close uids
deallocate uids