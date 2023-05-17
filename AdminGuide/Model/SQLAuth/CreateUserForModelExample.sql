use [master]

GO

if not exists(select * from syslogins where [name] = 'model_user')
create login [model_user] with password = 'model_password'

GO

use [BooksModel]

GO

if not exists(select * from sysusers where name = 'model_user')
create user [model_user] for login [model_user]

GO

exec sp_addrolemember 'db_owner', 'model_user'



