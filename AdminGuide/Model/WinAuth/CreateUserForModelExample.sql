use [master]

GO

if not exists(select * from syslogins where [name] = 'domain\model_win_user')
create login [domain\model_win_user] from windows

GO


use [BooksModel]

GO

if not exists(select * from sysusers where name = 'model_win_user')
create user [model_win_user] for login [domain\model_win_user]

GO

exec sp_addrolemember 'db_owner', 'model_win_user'



