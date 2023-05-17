use [master]

GO

if not exists(select * from syslogins where [name] = 'exporter_user')
create login [exporter_user] with password = 'exporter_password'

GO

use [BooksWebsite]

GO

if not exists(select * from sysusers where name = 'exporter_user')
create user [exporter_user] for login [exporter_user]

GO

exec sp_addrolemember 'db_datareader', 'exporter_user'

GO

use [BooksModel]

GO

if not exists(select * from sysusers where name = 'exporter_user')
create user [exporter_user] for login [exporter_user]

GO

exec sp_addrolemember 'db_datareader', 'exporter_user'
exec sp_addrolemember 'db_datawriter', 'exporter_user'


