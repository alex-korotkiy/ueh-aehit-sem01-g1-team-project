use [master]

GO

if not exists(select * from syslogins where [name] = 'domain\website_win_user')
create login [domain\website_win_user] from windows

GO

use [BooksWebsite]

go

if not exists(select * from sysusers where name = 'website_win_user')
create user [website_win_user] for login [domain\website_win_user]

go

exec sp_addrolemember 'db_owner', 'website_win_user'

