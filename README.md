# Bulletin Board

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
> dotnet run
```

Sqlite DB Schema:

![](./images/DbSchema.drawio.png)

## Demo

- Login Page:
  ![](./images/login.png)
- Register Page:
  ![](./images/register.png)
- Bulletin Board:
  ![](./images/bulletinboard.png)
- Reply a Post:
  ![](./images/reply.png)
