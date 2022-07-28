# Bulletin Board

API Docs: [https://jacklin-doxygen.herokuapp.com/](https://jacklin-doxygen.herokuapp.com/)

System Architecture: [SystemArchitecture.md](./Docs/SystemArchitecture/SystemArchitecture.md)

## How to Use

You can use the default `.db` or initialize database by:

```
> cd BulletinBoards
> sqlite3 .db
sqlite> .read scripts/init_sqlite.sql
sqlite> .exit
```

Run the appication:

```
> cd BulletinBoard
> dotnet restore
> dotnet run
```

Sqlite DB Schema:

![](./images/DbSchema.png)

## Demo

- Login Page:
  ![](./images/login.png)
- Register Page:
  ![](./images/register.png)
- Bulletin Board:
  ![](./images/bulletinboard.png)
- Reply a Post:
  ![](./images/reply.png)
