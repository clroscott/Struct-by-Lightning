CREATE TABLE [dbo].[ws_locations] (
    [LocationId]         INT            NOT NULL,
    [LocationName]       NVARCHAR (50)  NOT NULL,
    [RegionID]           INT            NULL,
    [CostRegionID]       INT            NULL,
    [PriceRegionID]      INT            NULL,
    [UnitNumber]         INT            NULL,
    [ManagerID]          INT            NULL,
    [OnWsbis]            BIT            NULL,
    [BidID]              INT            NULL,
    [Ownership]          NVARCHAR (20)  NULL,
    [IsUnion]            BIT            NULL,
    [Region]             NVARCHAR (50)  NULL,
    [BusinessConsultant] NVARCHAR (50)  NULL,
    [Concept]            NVARCHAR (20)  NULL,
    [Brand]              NVARCHAR (20)  NULL,
    [Area]               NVARCHAR (50)  NULL,
    [Comparable]         BIT            NULL,
    [Province]           NCHAR (10)     NULL,
    [Email]              NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

CREATE VIEW [dbo].[ws_locationView]
	AS SELECT LocationId, LocationName, BusinessConsultant, Province, Email, Concept FROM [ws_locations]


CREATE TABLE [dbo].[Form] (
    [FormID]       INT           NOT NULL,
    [DateCreated]  DATE          NOT NULL,
    [DateModified] DATE          NULL,
    [Concept]      NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([FormID] ASC)
);



CREATE TABLE [dbo].[SiteVisit] (
    [SiteVisitID]    INT  NOT NULL,
    [LocationID]     INT  NOT NULL,
    [FormID]         INT  NOT NULL,
    [dateOfVisit]    DATE NULL,
    [dateModified]   DATE NULL,
    [CommentPublic]  TEXT NULL,
    [CommentPrivate] TEXT NULL,
    [ManagerOnDuty]  NVARCHAR (50) NULL,
    [GeneralManager] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SiteVisitID] ASC),
    CONSTRAINT [FK_SiteVisit_Form] FOREIGN KEY ([FormID]) REFERENCES [dbo].[Form] ([FormID])
);



CREATE TABLE [dbo].[SiteActionItem] (
    [ActionID]     INT            NOT NULL,
    [SiteVisitID]  INT            NULL,
    [LocationID]   INT            NOT NULL,
    [Description]  NVARCHAR (200) NOT NULL,
    [DateCreated]  DATE           NOT NULL,
    [Complete]     BIT            DEFAULT ((0)) NOT NULL,
    [DateComplete] DATE           NULL,
    PRIMARY KEY CLUSTERED ([ActionID] ASC),
    CONSTRAINT [FK_SiteActionItem_SiteVisit] FOREIGN KEY ([SiteVisitID]) REFERENCES [dbo].[SiteVisit] ([SiteVisitID])
);



CREATE TABLE [dbo].[Section] (
    [SectionID]    INT        NOT NULL,
    [FormID]       INT        NOT NULL,
    [SectionName]  NCHAR (20) NOT NULL,
    [SectionOrder] INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([SectionID] ASC),
    CONSTRAINT [FK_Section_Form] FOREIGN KEY ([FormID]) REFERENCES [dbo].[Form] ([FormID])
);

CREATE TABLE [dbo].[Question] (
    [QuestionID]    INT            NOT NULL,
    [SectionID]     INT            NOT NULL,
    [QuestionName]  NVARCHAR (200) NOT NULL,
    [Active]        BIT            DEFAULT ((1)) NOT NULL,
    [QuestionOrder] INT            NULL,
    PRIMARY KEY CLUSTERED ([QuestionID] ASC),
    CONSTRAINT [FK_Question_Section] FOREIGN KEY ([SectionID]) REFERENCES [dbo].[Section] ([SectionID])
);

CREATE TABLE [dbo].[Answer] (
    [AnswerID]    INT            NOT NULL,
    [SiteVisitID] INT            NOT NULL,
    [QuestionID]  INT            NOT NULL,
    [Rating]      INT            NULL,
    [Comment]     NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([AnswerID] ASC),
    CONSTRAINT [FK_Answer_SiteVisit] FOREIGN KEY ([SiteVisitID]) REFERENCES [dbo].[SiteVisit] ([SiteVisitID]),
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([QuestionID])
);

INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (1, N'104 Kitsilano', 3, 1, 3, 104, 6, 1, NULL, N'Corporate', 0, N'Vancouver', N'Brett Hertzog', N'Free Stand', N'White Spot', N'LMV', 1, N'BC        ', N'unit104@whitespot.ca')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (2, N'106 Georgia & Cardero', 3, 1, 3, 106, 13, 1, NULL, N'Corporate', 0, N'Vancouver', N'Brett Hertzog', N'Free Stand', N'White Spot', N'LMV', 1, N'BC        ', N'unit106@whitespot.ca')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (3, N'108 Park Royal', 3, 1, 3, 108, 10, 1, NULL, N'Corporate', 0, N'North/West Vancouver', N'Joe Mandarino', N'Free Stand', N'White Spot', N'LMV', 1, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (4, N'111 SE Marine Drive', 3, 1, 3, 111, 7, 1, NULL, N'Corporate', 1, N'Vancouver', N'Brett Hertzog', N'Free Stand', N'White Spot', N'LMV', 1, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (5, N'115 Gilmore', 3, 1, 3, 115, NULL, NULL, NULL, NULL, NULL, N'Burnaby/New West', N'Joe Mandarino', N'Free Stand', NULL, NULL, NULL, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (6, N'117 North Road', 3, 1, 3, 117, NULL, NULL, NULL, NULL, NULL, N'Burnaby/New West', N'Joe Mandarino', N'Free Stand', NULL, NULL, NULL, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (7, N'118 Cambie & 13th', 3, 1, 3, 118, NULL, NULL, NULL, NULL, NULL, N'Vancouver', N'Brett Hertzog', N'Free Stand', NULL, NULL, NULL, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (8, N'119 Lonsdale', 3, 1, 3, 119, NULL, NULL, NULL, NULL, NULL, N'North/West', N'Joe Mandarino', N'Free Stand', NULL, NULL, NULL, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (9, N'120 Caledonia', NULL, NULL, NULL, 120, NULL, NULL, NULL, NULL, NULL, N'Victoria', N'Jennifer Moyou', N'Free Stand', NULL, NULL, NULL, N'BC        ', NULL)
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (10, N'1234 Fake Street', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Aaron', N'Free Stand', NULL, NULL, NULL, N'BC        ', N'aaron.selles@gmail.com')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (11, N'4321 Aquaman Avenue', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Clayton', N'Free Stand', NULL, NULL, NULL, N'BC        ', N'clroscott@gmail.com')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (12, N'5678 Anakin Avenue', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Aleeza', N'Free Stand', NULL, NULL, NULL, N'BC        ', N'aleeza.arcangel@gmail.com')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (13, N'1357 Real Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Doreen', N'Free Stand', NULL, NULL, NULL, N'BC        ', N'doreency@gmail.com')
INSERT INTO [dbo].[ws_locations] ([LocationId], [LocationName], [RegionID], [CostRegionID], [PriceRegionID], [UnitNumber], [ManagerID], [OnWsbis], [BidID], [Ownership], [IsUnion], [Region], [BusinessConsultant], [Concept], [Brand], [Area], [Comparable], [Province], [Email]) VALUES (14, N'0987 Lavish Lane', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Jung', N'Free Stand', NULL, NULL, NULL, N'BC        ', N'cstjungkim@gmail.com')



INSERT INTO [dbo].[Form] ([FormID], [DateCreated], [DateModified], [Concept]) VALUES (1, N'2013-11-01', NULL, NULL)

INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (0, 1, 1, N'0001-01-01', NULL, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (1, 1, 1, N'2013-01-11', N'2013-02-11', N'public', N'private', NULL, NULL)
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (2, 2, 1, N'2013-02-11', N'2013-03-22', N'public2', N'private2', NULL, NULL)
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (3, 1, 1, N'2013-11-16', NULL, N'wefwefwefewf
SDSDFSDF
!!!', N'fewewfewfewf
RERFEGSDF
!!!!', N'SDFSDFS                            ', N'DSGS                               ')
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (5, 1, 1, N'2013-11-16', N'0001-01-01', N'oops', N'I am smrt', N'cool                               ', N'story                              ')
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (6, 10, 1, N'2013-11-05', N'0001-01-01', N'aaron is a person', NULL, N'aaron                              ', N'blardh                             ')
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (7, 11, 1, N'2013-11-17', N'0001-01-01', N'ads', N'adsd', NULL, NULL)
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (8, 11, 1, N'2013-11-17', N'0001-01-01', N'this is public ', N'this is private', N'Hung', N'Paul')
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (9, 11, 1, N'2013-11-17', N'0001-01-01', N'fsafasdf', N'sdfaafsd', N'pwopo', N'wqegyifd')
INSERT INTO [dbo].[SiteVisit] ([SiteVisitID], [LocationID], [FormID], [dateOfVisit], [dateModified], [CommentPublic], [CommentPrivate], [ManagerOnDuty], [GeneralManager]) VALUES (10, 11, 1, N'2013-11-17', N'0001-01-01', N'tiuir', N'fdshgfdig', N'wter', N'g')

INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (1, 1, 1, N'testing1', N'2013-01-11', 0, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (2, 2, 1, N'testing2', N'2013-02-11', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (3, 0, 1, N'thing', N'2013-11-16', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (4, 0, 1, N'other item', N'2013-11-16', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (5, 0, 1, N'aleeza', N'2013-11-16', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (6, 0, 1, N'work', N'2013-11-16', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (7, 0, 1, N'hello', N'2013-11-16', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (8, 0, 1, N'hello', N'2013-11-16', 0, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (9, 0, 11, N'thing', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (10, 0, 11, N'other', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (11, 0, 11, N'ashdofadsfusfadads', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (12, 0, 11, N'aleeza', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (13, NULL, 11, N'heyyyyy', N'2013-11-17', 0, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (14, NULL, 11, N'whaaaa', N'2013-11-17', 1, N'2013-11-19')
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (15, NULL, 11, N'looool', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (16, NULL, 11, N'leeelll', N'2013-11-17', 1, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (17, NULL, 11, N'bloooo', N'2013-11-17', 1, N'2013-11-19')
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (18, 8, 11, N'important', N'2013-11-17', 0, NULL)
INSERT INTO [dbo].[SiteActionItem] ([ActionID], [SiteVisitID], [LocationID], [Description], [DateCreated], [Complete], [DateComplete]) VALUES (19, 8, 11, N'other thing', N'2013-11-17', 1, NULL)

INSERT INTO [dbo].[Section] ([SectionID], [FormID], [SectionName], [SectionOrder]) VALUES (1, 1, N'Exterior            ', 2)
INSERT INTO [dbo].[Section] ([SectionID], [FormID], [SectionName], [SectionOrder]) VALUES (2, 1, N'Front of House      ', 3)
INSERT INTO [dbo].[Section] ([SectionID], [FormID], [SectionName], [SectionOrder]) VALUES (3, 1, N'Back of House       ', 4)
INSERT INTO [dbo].[Section] ([SectionID], [FormID], [SectionName], [SectionOrder]) VALUES (4, 1, N'Overall             ', 5)

INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (1, 1, N'Parking Lot', 1, 1)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (2, 1, N'Signage', 1, 2)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (3, 1, N'Building Exterior', 1, 3)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (4, 1, N'Back Door', 1, 4)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (5, 1, N'Garbage Enclosure', 1, 5)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (6, 1, N'General Cleanliness of Exterior', 1, 6)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (7, 1, N'Landscaping', 1, 7)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (8, 1, N'Sidewalks and Walkways', 1, 8)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (9, 1, N'Patio', 1, 9)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (10, 1, N'Front Door', 1, 10)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (11, 2, N'Lobby', 1, 1)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (12, 2, N'Furniture', 1, 2)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (13, 2, N'Floors', 1, 3)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (14, 2, N'Lighting', 1, 4)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (15, 2, N'Cleanliness', 1, 5)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (16, 2, N'Employee Uniforms', 1, 6)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (17, 2, N'Equipment', 1, 7)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (18, 2, N'Bar/Lounge', 1, 8)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (19, 2, N'Restrooms', 1, 9)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (20, 2, N'Menus', 1, 10)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (21, 2, N'Marketing Materials', 1, 11)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (22, 2, N'Back Bar areas', 1, 12)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (23, 2, N'Pre-Shift Meetings', 1, 13)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (24, 3, N'Equipment', 1, 1)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (25, 3, N'Line Check', 1, 2)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (26, 3, N'Food Quality Prep/Online', 1, 3)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (27, 3, N'Main Cooler', 1, 4)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (28, 3, N'Main Freezer', 1, 5)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (29, 3, N'On-Line Refrigeration', 1, 6)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (30, 3, N'Food Quality Pass through', 1, 7)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (31, 3, N'Temperature Checks', 1, 8)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (32, 3, N'Shelf Life Standards followed', 1, 9)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (33, 3, N'Uniforms', 1, 10)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (34, 4, N'Quality', 1, 1)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (35, 4, N'Service', 1, 2)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (36, 4, N'Management Presence', 1, 3)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (37, 4, N'Energy of Staff', 1, 4)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (38, 4, N'Product Knowledge of Staff', 1, 5)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (39, 4, N'Atmosphere', 1, 6)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (40, 4, N'Empathica Results', 1, 7)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (41, 4, N'Communication', 1, 8)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (42, 4, N'Salesmanship', 1, 9)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (43, 4, N'Staffing Levels', 1, 10)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (44, 4, N'Food Quality overall', 1, 11)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (45, 4, N'Coaching During Service', 1, 12)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (46, 4, N'Drive-in Service', 1, 13)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (47, 4, N'Take-out Service', 1, 14)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (48, 4, N'New Hires Training', 1, 15)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (49, 4, N'Red Book utilized to max', 1, 16)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (50, 4, N'Health & Safety', 1, 17)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (51, 4, N'Food and Labour Cost', 1, 18)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (52, 4, N'Ops Review Readiness', 1, 19)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (53, 4, N'Financial Discussion', 1, 20)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (54, 4, N'Local Store Marketing', 1, 21)
INSERT INTO [dbo].[Question] ([QuestionID], [SectionID], [QuestionName], [Active], [QuestionOrder]) VALUES (55, 4, N'Brand Standards Followed', 1, 22)

