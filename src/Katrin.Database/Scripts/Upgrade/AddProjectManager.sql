
--DECLARE @RoleId uniqueidentifier
--SET	@RoleId = NEWID()
--INSERT INTO [dbo].[Role]
--           (RoleId
--           ,[RoleName])
--VALUES	(@RoleId
--		   ,'Project Manager')

--INSERT INTO [dbo].[RolePrivilege]
--           ([RolePrivilegeId]
--           ,[RoleId]
--           ,[PrivilegeId])
--SELECT	[RolePrivilegeId]	= NEWID(),
--        [RoleId]			= @RoleId,
--		[PrivilegeId]		= p.PrivilegeId
--FROM	Privilege p
--		INNER JOIN PrivilegeEntity pe
--		ON	pe.PrivilegeId	= p.PrivilegeId
--WHERE	pe.EntityName IN('ProjectTask') AND p.AccessRight = 8