Create table T_Game_Sessions
(
	SessionId bigint IDENTITY(1,1) primary key,
	SessionPin varchar(6) not null,	
	CreateTime datetime not null,
	SessionIsActive bit not null
)


Create table T_Game_Session_Players
(
	Id bigint IDENTITY(1,1) primary key,
	SessionId bigint not null FOREIGN KEY REFERENCES T_Game_Sessions(SessionId),
	PlayerName varchar(250) not null,
	JoinTime datetime not null,
	DeviceId varchar(100) not null,
	RequestAppId varchar(50) not null
	
)

