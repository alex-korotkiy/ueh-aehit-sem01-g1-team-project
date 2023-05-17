use [master]

GO

if not exists(select * from syslogins where [name] = 'domain\exporter_win_user')
create login [domain\exporter_win_user] from windows

GO

use [BooksWebsite]

GO

if not exists(select * from sysusers where name = 'exporter_win_user')
create user [exporter_win_user] for login [domain\exporter_win_user]

GO

exec sp_addrolemember 'db_datareader', 'exporter_win_user'

GO

use [BooksModel]

GO

if not exists(select * from sysusers where name = 'exporter_win_user')
create user [exporter_win_user] for login [domain\exporter_win_user]

GO

exec sp_addrolemember 'db_datareader', 'exporter_win_user'
exec sp_addrolemember 'db_datawriter', 'exporter_win_user'


