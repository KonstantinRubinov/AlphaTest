MySql

CREATE DATABASE AlphaTest;

CREATE TABLE AlphaTest.GROUPS (
	groupID int AUTO_INCREMENT,
	groupName nvarchar(50) NOT NULL,
    PRIMARY KEY ( groupID )
);

CREATE TABLE AlphaTest.USERS (
	userID int AUTO_INCREMENT,
	userName nvarchar(50) NOT NULL,
	userGroupId int NOT NULL,
	userNumberOfFollowers int NOT NULL,
	userPassword nvarchar(50) NOT NULL,
    PRIMARY KEY ( userID ),
	FOREIGN KEY (userGroupId) REFERENCES alphatest.GROUPS (groupID)
);

CREATE TABLE AlphaTest.FOLLOWS (
	followCounter int AUTO_INCREMENT,
	followedID int NOT NULL,
	followerID int NOT NULL,
    PRIMARY KEY ( followCounter ),
	FOREIGN KEY ( followedID ) REFERENCES alphatest.USERS (userID),
	FOREIGN KEY ( followerID ) REFERENCES alphatest.USERS (userID)
);


Delimiter $$
CREATE PROCEDURE AlphaTest.ReturnUserByNamePassword (IN userName varchar(50), IN userPassword varchar(50))
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		SELECT * from Users WHERE Users.userName=userName and Users.userPassword=userPassword;
	COMMIT;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE AlphaTest.GetOneUserById (IN userID int)
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		SELECT * from Users WHERE Users.userID=userID;
	COMMIT;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE AlphaTest.GetAllUsers ()
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		SELECT * from Users;
	COMMIT;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE AlphaTest.IsFollowedByMe (IN followedID int, IN followerID int)
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		SELECT COUNT(1) FROM FOLLOWS WHERE FOLLOWS.followedID = followedID and FOLLOWS.followerID=followerID;
	COMMIT;
END $$
Delimiter ;



Delimiter $$
CREATE PROCEDURE AlphaTest.AddFollower (IN followedID int, IN followerID int)
BEGIN
	DECLARE IncrementValue int;
    DECLARE followedByMe int;
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		INSERT INTO FOLLOWS (followedID, followerID) VALUES (followedID, followerID);
		SET IncrementValue = 1;
		UPDATE USERS SET USERS.userNumberOfFollowers = userNumberOfFollowers + IncrementValue WHERE USERS.userID = followedID;
		SET followedByMe = (select COUNT(1) FROM FOLLOWS
		WHERE FOLLOWS.followedID = followedID and FOLLOWS.followerID=followerID);
		SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, followedByMe
        from USERS
        LEFT JOIN alphatest.GROUPS ON USERS.userGroupId=alphatest.GROUPS.groupID
        WHERE USERS.userID = followedID;
	COMMIT;
END $$
Delimiter ;





Delimiter $$
CREATE PROCEDURE AlphaTest.DeleteFollower (IN followedID int, IN followerID int)
BEGIN
	DECLARE IncrementValue int;
    DECLARE followedByMe int;
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		DELETE FROM alphatest.FOLLOWS WHERE alphatest.FOLLOWS.followedID = followedID and alphatest.FOLLOWS.followerID=followerID;
		SET IncrementValue = 1;
		UPDATE USERS SET USERS.userNumberOfFollowers = userNumberOfFollowers - IncrementValue WHERE USERS.userID = followedID;
		SET followedByMe = (select COUNT(1) FROM FOLLOWS
		WHERE FOLLOWS.followedID = followedID and FOLLOWS.followerID=followerID);
        SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, followedByMe
        from USERS
        LEFT JOIN alphatest.GROUPS ON USERS.userGroupId=alphatest.GROUPS.groupID
        WHERE USERS.userID = followedID;
	COMMIT;
END $$
Delimiter ;



DELIMITER $$
CREATE PROCEDURE AlphaTest.GetUsersForTable (IN myId int)
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
        SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers,
		(select COUNT(1) FROM FOLLOWS
        WHERE FOLLOWS.followedID = USERS.userID and FOLLOWS.followerID=myId) followedByMe
		from alphatest.USERS
		LEFT JOIN alphatest.GROUPS ON alphatest.USERS.userGroupId=alphatest.GROUPS.groupID;
	COMMIT;
END$$
DELIMITER ;



Delimiter $$
CREATE PROCEDURE AlphaTest.GetOneUserForTableById (IN followedID int, IN followerID int)
BEGIN
	DECLARE followedByMe int;
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
	START TRANSACTION;
		SET followedByMe = (select COUNT(1) FROM alphatest.FOLLOWS WHERE alphatest.FOLLOWS.followedID = followedID and FOLLOWS.followerID=followerID);
		SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, followedByMe
		from alphatest.USERS
		LEFT JOIN alphatest.GROUPS ON alphatest.USERS.userGroupId=alphatest.GROUPS.groupID
		WHERE alphatest.USERS.userID = followedID;
	COMMIT;
END $$
Delimiter ;

//-------------------------------------------------------------------------------------------------------------------------//
Sql

CREATE DATABASE AlphaTest;


Use AlphaTest
Go

CREATE TABLE GROUPS (
	groupID int primary key identity,
	groupName nvarchar(50) NOT NULL
);

CREATE TABLE USERS (
	userID int primary key identity,
	userName nvarchar(50) NOT NULL,
	userGroupId int NOT NULL FOREIGN KEY REFERENCES GROUPS(groupID),
	userNumberOfFollowers int NOT NULL,
	userPassword nvarchar(50) NOT NULL
);

CREATE TABLE FOLLOWS (
	followCounter int primary key identity,
	followedID int NOT NULL FOREIGN KEY REFERENCES USERS(userID),
	followerID int NOT NULL FOREIGN KEY REFERENCES USERS(userID)
);


Use AlphaTest
Go

CREATE PROCEDURE [dbo].ReturnUserByNamePassword (@userName nvarchar(50), @userPassword nvarchar(50))
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT * from Users WHERE userName=@userName and userPassword=@userPassword;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch;
	
	
CREATE PROCEDURE [dbo].GetOneUserById (@userID int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT * from Users WHERE userID=@userID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch;


CREATE PROCEDURE [dbo].GetAllUsers
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT * from Users;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch

CREATE PROCEDURE [dbo].IsFollowedByMe (@followedID int, @followerID int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT COUNT(1) FROM FOLLOWS WHERE followedID = @followedID and followerID=@followerID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch

CREATE PROCEDURE [dbo].AddFollower (@followedID int, @followerID int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			INSERT INTO FOLLOWS (followedID, followerID) VALUES (@followedID, @followerID);
			DECLARE @IncrementValue int
			SET @IncrementValue = 1
			UPDATE USERS SET userNumberOfFollowers = userNumberOfFollowers + @IncrementValue WHERE userID = @followedID;
			SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(*) AS BIT) FROM [FOLLOWS] WHERE (followedID = USERS.userID and followerID=@followerID)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID WHERE USERS.userID = @followedID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch


CREATE PROCEDURE [dbo].DeleteFollower (@followedID int, @followerID int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			DELETE FROM FOLLOWS WHERE followedID = @followedID and followerID=@followerID;
			DECLARE @IncrementValue int
			SET @IncrementValue = 1
			UPDATE USERS SET userNumberOfFollowers = userNumberOfFollowers - @IncrementValue WHERE userID = @followedID;
			SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(*) AS BIT) FROM [FOLLOWS] WHERE (followedID = USERS.userID and followerID=@followerID)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID WHERE USERS.userID = @followedID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
	
	
	
CREATE PROCEDURE [dbo].GetUsersForTable (@myId int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(*) AS BIT) FROM [FOLLOWS] WHERE (followedID = USERS.userID and followerID=@myId)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
CREATE PROCEDURE [dbo].GetOneUserForTableById (@followedID int, @followerID int)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			SELECT USERS.userID, USERS.userName, GROUPS.groupName, USERS.userNumberOfFollowers, (select CAST(COUNT(*) AS BIT) FROM [FOLLOWS] WHERE (followedID = @followedID and followerID=@followerID)) as followedByMe from USERS LEFT JOIN GROUPS ON USERS.userGroupId=GROUPS.groupID WHERE USERS.userID = @followedID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
	
	
	
	
	
	
	
INSERT INTO AlphaTest.GROUPS (groupName) VALUES ('Group1');
INSERT INTO AlphaTest.GROUPS (groupName) VALUES ('Group2');
INSERT INTO AlphaTest.GROUPS (groupName) VALUES ('Group3');
INSERT INTO AlphaTest.GROUPS (groupName) VALUES ('Group4');
INSERT INTO AlphaTest.GROUPS (groupName) VALUES ('Group5');

INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user01', 1, 0, 'user01');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user02', 2, 0, 'user02');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user03', 3, 0, 'user03');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user04', 4, 0, 'user04');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user05', 5, 0, 'user05');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user06', 1, 0, 'user06');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user07', 2, 0, 'user07');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user08', 3, 0, 'user08');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user09', 4, 0, 'user09');
INSERT INTO AlphaTest.USERS (userName, userGroupId, userNumberOfFollowers, userPassword) VALUES ('user10', 5, 0, 'user10');
