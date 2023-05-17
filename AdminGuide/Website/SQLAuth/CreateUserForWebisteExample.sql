use [master]

GO

if not exists(select * from syslogins where [name] = 'website_user')
create login [website_user] with password = 'website_password'

GO

use [BooksWebsite]

go

if not exists(select * from sysusers where name = 'website_user')
create user [website_user] for login [website_user]

go

exec sp_addrolemember 'db_owner', 'website_user'

