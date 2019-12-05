use CayGiaPhaAPI
--Dong bo khoa ngoai Node_Id va IdNodeCha
create trigger trg_NodeParentId
on Nodes
after insert, update
as
begin
	declare @parentId int, @nodeId int, @temp1 int, @temp2 int
	
	select @temp1 = Node_Id
	from inserted

	select @temp2 = IdNodeCha
	from inserted
	
	if(@temp1 is not null)
		set @parentId = @temp1
	else if (@temp2 is not null)
		set @parentId = @temp2


	select @nodeId = Id
	from inserted

	update Nodes
	set IdNodeCha = @parentId, Node_Id = @parentId
	where Id = @nodeId
	
end

GO

--Dong bo khoa ngoai Node_Id va IdNodeCha
create trigger trg_PersonNodeId
on People
after insert, update
as
begin
	declare @parentId int, @personId int, @temp1 int, @temp2 int
	
	select @temp1 = Node_Id
	from inserted

	select @temp2 = IdNodeCha
	from inserted
	
	if(@temp1 is not null)
		set @parentId = @temp1
	else if (@temp2 is not null)
		set @parentId = @temp2

	select @personId = personId
	from inserted

	update People
	set IdNodeCha = @parentId, Node_Id = @parentId
	where personId = @personId
end

--Xoa person thi xoa cac node lien quan den person
alter trigger trg_DeletePerson
on People
after delete
as
begin
	declare @personId int, @nodeId int
	select @personId = personId
	from deleted

	select @nodeId = Node_Id
	from deleted 
	
	--Neu Node chi co 1 person (Node cha chi co 1 nguoi)
	if(select count(*) from People where IdNodeCha =@nodeId)=0		
	BEGIN
		--delete person dau tien
		delete from People
		where personId = @personId
		--delete person la child cua person dau tien
		delete from People
		where IdNodeCha in (select id
							from Nodes a , People b
							where a.IdNodeCha = @nodeId and a.Id = b.IdNodeCha)
		--delete cac node lien quan

		delete from Nodes
		where IdNodeCha = @nodeId 

		delete from Nodes
		where Id = @nodeId
	END
	--Node co nhieu hon 1 person (Node cha co tren 1 nguoi)
	else
	begin
		delete from People
		where personId = @personId
	end
end

delete from people
where personId = 5

select * from people
select * from nodes

select count(*) from People where IdNodeCha =48
