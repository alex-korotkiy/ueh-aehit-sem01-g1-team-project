use [master]

GO

if not exists(select * from syslogins where [name] = 'domain\importer_win_user')
create login [domain\importer_win_user] from windows

GO

use [BooksWebsite]

GO

if not exists(select * from sysusers where name = 'importer_win_user')
create user [importer_win_user] for login [domain\importer_win_user]

GO

exec sp_addrolemember 'db_owner', 'importer_win_user'

GO

use [BooksModel]

GO

if not exists(select * from sysusers where name = 'importer_win_user')
create user [importer_win_user] for login [domain\importer_win_user]

GO

exec sp_addrolemember 'db_datareader', 'importer_win_user'



