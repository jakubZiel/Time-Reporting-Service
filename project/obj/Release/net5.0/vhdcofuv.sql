CREATE TABLE [Employee] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(40) NULL,
    [Surname] nvarchar(40) NULL,
    [Password] nvarchar(50) NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY ([ID])
);
GO


CREATE TABLE [Project] (
    [ID] int NOT NULL IDENTITY,
    [OwnerID] int NULL,
    [Name] nvarchar(40) NULL,
    [TimeBudget] int NOT NULL,
    [Active] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Project_Employee_OwnerID] FOREIGN KEY ([OwnerID]) REFERENCES [Employee] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Report] (
    [ID] int NOT NULL IDENTITY,
    [EmployeeID] int NULL,
    [Month] datetime2 NOT NULL,
    [Frozen] bit NOT NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Report_Employee_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employee] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [EmployeeProject] (
    [ID] int NOT NULL IDENTITY,
    [EmployeeID] int NULL,
    [ProjectID] int NULL,
    CONSTRAINT [PK_EmployeeProject] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_EmployeeProject_Employee_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employee] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EmployeeProject_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [Project] ([ID]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Tag] (
    [ID] int NOT NULL IDENTITY,
    [ProjectID] int NOT NULL,
    [Name] nvarchar(40) NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Tag_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [Project] ([ID]) ON DELETE CASCADE
);
GO


CREATE TABLE [Activity] (
    [ID] int NOT NULL IDENTITY,
    [ProjectID] int NOT NULL,
    [EmployeeID] int NOT NULL,
    [ReportID] int NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (getdate()),
    [AcceptedTime] int NULL,
    [DurationMinutes] int NOT NULL,
    [Name] nvarchar(40) NULL,
    [Frozen] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    [Tag] nvarchar(40) NULL,
    [Timestamp] rowversion NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Activity_Employee_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employee] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Activity_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [Project] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Activity_Report_ReportID] FOREIGN KEY ([ReportID]) REFERENCES [Report] ([ID]) ON DELETE NO ACTION
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Name', N'Password', N'Surname') AND [object_id] = OBJECT_ID(N'[Employee]'))
    SET IDENTITY_INSERT [Employee] ON;
INSERT INTO [Employee] ([ID], [Name], [Password], [Surname])
VALUES (1, N'Jakub', N'123', N'Zielinski'),
(2, N'Piotr', N'1234', N'Lewandowski'),
(3, N'Waldemar', N'12345', N'Grabski'),
(4, N'Krzysztof', N'123456', N'Chabko');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Name', N'Password', N'Surname') AND [object_id] = OBJECT_ID(N'[Employee]'))
    SET IDENTITY_INSERT [Employee] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] ON;
INSERT INTO [Project] ([ID], [Active], [Description], [Name], [OwnerID], [TimeBudget])
VALUES (1, CAST(1 AS bit), N'Some React fullstack application', N'ReactApp', 1, 1500);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] ON;
INSERT INTO [Project] ([ID], [Active], [Description], [Name], [OwnerID], [TimeBudget])
VALUES (2, CAST(1 AS bit), N'Some Vue.Js frontend application', N'VueApp', 2, 2200);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] ON;
INSERT INTO [Project] ([ID], [Active], [Description], [Name], [OwnerID], [TimeBudget])
VALUES (3, CAST(1 AS bit), N'Some Spring Boot backend application', N'Spring Boot App', 4, 1600);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Active', N'Description', N'Name', N'OwnerID', N'TimeBudget') AND [object_id] = OBJECT_ID(N'[Project]'))
    SET IDENTITY_INSERT [Project] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'AcceptedTime', N'DateCreated', N'Description', N'DurationMinutes', N'EmployeeID', N'Frozen', N'Name', N'ProjectID', N'ReportID', N'Tag') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] ON;
INSERT INTO [Activity] ([ID], [AcceptedTime], [DateCreated], [Description], [DurationMinutes], [EmployeeID], [Frozen], [Name], [ProjectID], [ReportID], [Tag])
VALUES (1, NULL, '2022-01-20T00:00:00.0000000+01:00', N'checking if everything is ok with the API', 30, 1, CAST(0 AS bit), N'API debugging', 1, NULL, N'debugging');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'AcceptedTime', N'DateCreated', N'Description', N'DurationMinutes', N'EmployeeID', N'Frozen', N'Name', N'ProjectID', N'ReportID', N'Tag') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Name', N'ProjectID') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] ON;
INSERT INTO [Tag] ([ID], [Name], [ProjectID])
VALUES (1, N'coding', 1),
(2, N'debuging', 1),
(3, N'database', 2),
(4, N'coding', 2),
(5, N'drinking', 3),
(6, N'coding', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Name', N'ProjectID') AND [object_id] = OBJECT_ID(N'[Tag]'))
    SET IDENTITY_INSERT [Tag] OFF;
GO


CREATE INDEX [IX_Activity_EmployeeID] ON [Activity] ([EmployeeID]);
GO


CREATE INDEX [IX_Activity_ProjectID] ON [Activity] ([ProjectID]);
GO


CREATE INDEX [IX_Activity_ReportID] ON [Activity] ([ReportID]);
GO


CREATE INDEX [IX_EmployeeProject_EmployeeID] ON [EmployeeProject] ([EmployeeID]);
GO


CREATE INDEX [IX_EmployeeProject_ProjectID] ON [EmployeeProject] ([ProjectID]);
GO


CREATE INDEX [IX_Project_OwnerID] ON [Project] ([OwnerID]);
GO


CREATE INDEX [IX_Report_EmployeeID] ON [Report] ([EmployeeID]);
GO


CREATE INDEX [IX_Tag_ProjectID] ON [Tag] ([ProjectID]);
GO


