CREATE TABLE Users (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  DisplayName TEXT,
  RegisterDate TEXT NOT NULL,
  Password TEXT NOT NULL
);

INSERT INTO Users (
  Name, 
  DisplayName,
  RegisterDate,
  Password
) VALUES (
  'Lin',
  'Jack',
  '2022-07-18 00:00:00',
  '1234'
);

SELECT * FROM Users;

CREATE TABLE Posts (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  UserId INTEGER NOT NULL,
  SubmitTime TEXT NOT NULL,
  Text TEXT NOT NULL,
  FOREIGN KEY(UserId) REFERENCES Users(Id)
);

INSERT INTO Posts (
  UserId, 
  SubmitTime,
  Text
) VALUES (
  '1',
  '2022-07-18 00:01:00',
  'Hello World!'
);

SELECT * FROM Posts;

CREATE TABLE Replies (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  PostId INTEGER NOT NULL,
  UserId INTEGER NOT NULL,
  SubmitTime TEXT NOT NULL,
  Text TEXT NOT NULL,
  FOREIGN KEY(PostId) REFERENCES Posts(Id),
  FOREIGN KEY(UserId) REFERENCES Users(Id)
);

INSERT INTO Replies (
  PostId,
  UserId,
  SubmitTime,
  Text
) VALUES (
  '1',
  '1',
  '2022-07-18 00:01:00',
  'A reply from Jack!'
);

SELECT * FROM Replies;