use CayGiaPhaAPI
--Dong bo khoa ngoai Node_Id va IdNodeCha
go
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

--Dong bo khoa ngoai Node_Id va IdNodeCha
GO
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
GO
CREATE trigger trg_DeletePerson
on People
after delete
as
begin
	declare @personId int, @nodeId int
	select @personId = personId
	from deleted
	select @nodeId = Node_Id
	from deleted 
	if(@nodeId = 1)
	begin
		raiserror(N'Không được xoá node này!!!',16,1)
		rollback
		return
	end
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

GO
SET IDENTITY_INSERT Nodes ON
GO
INSERT INTO nodes(Id,IdNodeCha)
VALUES (1,null)
GO
INSERT INTO nodes(Id,IdNodeCha)
VALUES (2,1)
GO
INSERT INTO nodes(Id,IdNodeCha)
VALUES (3,1)
GO
SET IDENTITY_INSERT Nodes OFF
GO
SET IDENTITY_INSERT People ON
GO
INSERT INTO People(personId,personGender,personImg,personName,IdNodeCha)
VALUES(1,'male','/UploadedFiles/male.png','Parent',1)
GO
INSERT INTO People(personId,personGender,personImg,personName,IdNodeCha)
VALUES(2,'female','/UploadedFiles/female.png','Child',2)
GO
INSERT INTO People(personId,personGender,personImg,personName,IdNodeCha)
VALUES(3,'male','/UploadedFiles/male.png','Child',3)
GO
SET IDENTITY_INSERT People OFF

