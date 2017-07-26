
CREATE TABLE [jcms_extends_visitlogs] (
	[Id] AutoIncrement primary key ,
	[VisitIp] varchar(16) NULL,
	[VisitTime] datetime NOT NULL DEFAULT NOW(),
	[VisitCountry] varchar(20) NULL,
	[VisitIplocal] varchar(80) NULL,
	[VisitReferer] varchar(200) NULL,
	[VisitMethod] varchar(50) NULL
)
GO
