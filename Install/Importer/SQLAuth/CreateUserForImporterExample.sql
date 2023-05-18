use [master]

GO

if not exists(select * from syslogins where [name] = 'importer_user')
create login [importer_user] with password = 'importer_password'

GO

use [BooksWebsite]

GO

if not exists(select * from sysusers where name = 'importer_user')
create user [importer_user] for login [importer_user]

GO

exec sp_addrolemember 'db_owner', 'importer_user'

GO

use [BooksModel]

GO

if not exists(select * from sysusers where name = 'importer_user')
create user [importer_user] for login [importer_user]

GO

exec sp_addrolemember 'db_datareader', 'importer_user'



