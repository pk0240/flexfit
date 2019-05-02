
DROP TABLE SkillTreeBranchProgress
DROP TABLE Skill
DROP TABLE SkillTreeBranch
DROP TABLE DailyStepStats
DROP TABLE Level
DROP TABLE GameAccountState
DROP TABLE Login
DROP TABLE GameUser


/* ------------------------ GAME USER -------------------------*/
CREATE TABLE GameUser (
	user_ID			VARCHAR(8)		 PRIMARY KEY,
	fitbit_ID		CHAR(6)		 UNIQUE,
	age				int			 NOT NULL,
	currentWeight	decimal(4,2) NOT NULL,
	heightCM		decimal(4,2) NOT NULL,
	BMI				decimal(4,2) NOT NULL,
	totalStepCount	int			 NOT NULL
)

--INSERT INTO GameUser VALUES('11111111', '111111', 0, 0, 0, 0, 0);

INSERT INTO GameUser VALUES ('USR_1001', '23FLFQ', 10, 35.5, 1.34, 22.0, 15000);
INSERT INTO GameUser VALUES ('USR_1002', '56HHTR', 12, 40.0, 1.5, 24.0, 100);

--SELECT * FROM GameUser

/* --------------------------- LOGIN ----------------------------*/
CREATE TABLE Login (
	login_ID CHAR(8)	 PRIMARY KEY,
	username VARCHAR(20) UNIQUE,
	password VARCHAR(20) NOT NULL,
	user_ID VARCHAR(8)		 NOT NULL,
	FOREIGN KEY (user_ID) REFERENCES GameUser(user_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)

INSERT INTO Login VALUES ('LGN_1001', 'login1', 'pwd1', 'USR_1001');
INSERT INTO Login VALUES ('LGN_1002', 'login2', 'pwd2', 'USR_1002');

--SELECT * FROM Login

/* --------------------- GAME ACCOUNT STATE -----------------------*/
CREATE TABLE GameAccountState (
	gameAccount_ID			 CHAR(8)	  PRIMARY KEY,
	numberOfStepCoins		 INT		  NOT NULL,
	highestLevelCompleted	 INT,
	currentSpeed			 INT,
	currentJump				 INT,
	stepCountLeaderBoardRank INT,
	alreadyUpdated			 INT,
	lastAccessed			 VARCHAR(20),
	token					 VARCHAR(100) NOT NULL,
	user_ID					 VARCHAR(8)	  NOT NULL,
	FOREIGN KEY (user_ID) REFERENCES GameUser(user_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)

--UPDATE GameAccountState SET lastAccessed = '2018-03-03' WHERE  user_ID='6VP2XJ';
--delete from GameAccountState WHERE user_ID='6SNRHP';

INSERT INTO GameAccountState VALUES ('ACC_1001', 4000, 4, 1, 1, 300, 0, '2018-07-07', 'idk', 'USR_1001');
INSERT INTO GameAccountState VALUES ('ACC_1002', 5000, 8, 1, 1, 250, 0, '2018-07-07', 'idk', 'USR_1002');

--select currentSpeed from GameAccountState WHERE USER_ID = '6SNRHP';
--SELECT * FROM GameAccountState

/* ------------------------ LEVEL -------------------------*/
CREATE TABLE Level (
	level_ID			 CHAR(8)	 PRIMARY KEY,
	levelNumber			 INT		 NOT NULL,
	fastestTime			 TIME,
	levelLeaderboardRank INT,
	gameAccount_ID		 CHAR(8)	 NOT NULL,
	FOREIGN KEY (gameAccount_ID) REFERENCES GameAccountState(gameAccount_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)

INSERT INTO Level VALUES ('LVL1_001', 1, '12:34:54', 300, 'ACC_1001');
INSERT INTO Level VALUES ('LVL2_001', 2, '02:34:54', 30, 'ACC_1001');
INSERT INTO Level VALUES ('LVL3_001', 3, '00:22:50', 2, 'ACC_1001');

INSERT INTO Level VALUES ('LVL1_002', 1, '06:44:22', 100, 'ACC_1002');
INSERT INTO Level VALUES ('LVL2_002', 2, '00:15:01', 100, 'ACC_1002');

--SELECT * FROM Level


/* ------------------------ DAILY STEP STATS -------------------------*/
CREATE TABLE DailyStepStats (
	dailyReport_ID		CHAR(8)	 PRIMARY KEY,
	date				DATE,
	dailyStepCount		INT,
	user_ID				VARCHAR(8),
	FOREIGN KEY (user_ID) REFERENCES GameUser(user_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)

INSERT INTO DailyStepStats VALUES ('DR_00001', '2018-01-06', 1500, 'USR_1001')
INSERT INTO DailyStepStats VALUES ('DR_00002', '2018-02-06', 300, 'USR_1001')
INSERT INTO DailyStepStats VALUES ('DR_00003', '2018-08-17', 300, 'USR_1002')

--SELECT * FROM DailyStepStats

/* ------------------------- SKILL TREE BRANCH --------------------------*/
CREATE TABLE SkillTreeBranch (
	skillTreeBranch_ID		CHAR(8)	 PRIMARY KEY,
	branchName				VARCHAR(20)	 NOT NULL,
	description				VARCHAR(50)  NOT NULL
)

INSERT INTO SkillTreeBranch VALUES ('SKTREE_1', 'Speed', 'Tracks the speed of the character.')
INSERT INTO SkillTreeBranch VALUES ('SKTREE_2', 'Jump', 'Tracks the jump height of the character.')
INSERT INTO SkillTreeBranch VALUES ('SKTREE_3', 'Stamina', 'Tracks the stamina of the character.')

--SELECT * FROM SkillTreeBranch

/* ------------------------- SKILL --------------------------*/
CREATE TABLE Skill (
	skill_ID				CHAR(8)		 PRIMARY KEY,
	skillName				VARCHAR(20)	 NOT NULL,
	description				VARCHAR(100) NOT NULL,
	levelOnBranch			INT			 NOT NULL,
	skillStrengthLevel		DECIMAL(3,1) NOT NULL,
	cost					DECIMAL(7,2) NOT NULL,
	skillTreeBranch_ID		CHAR(8),
	FOREIGN KEY (skillTreeBranch_ID) REFERENCES SkillTreeBranch(skillTreeBranch_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)



-- first 5 attributes for speed
INSERT INTO Skill VALUES ('SKL_1001', 'Starting Speed', 'The starting pace for speed.', 1, 6.0, 1000.00, 'SKTREE_1')
INSERT INTO Skill VALUES ('SKL_1002', 'Jog', 'Can run slightly faster.', 2, 6.5, 2500.00, 'SKTREE_1')
INSERT INTO Skill VALUES ('SKL_1003', 'Run', 'Can run much faster.', 3, 7.0, 5000.00, 'SKTREE_1')
INSERT INTO Skill VALUES ('SKL_1004', 'Sprint', 'Can run extremely fast.', 4, 7.5, 7500.00, 'SKTREE_1')
INSERT INTO Skill VALUES ('SKL_1005', 'Power Run', 'Can bolt through the level at lightening speed.', 5, 8.0, 10000.00, 'SKTREE_1')

-- first 5 attributes for jump
INSERT INTO Skill VALUES ('SKL_1006', 'Start Jump', 'The starting height for jumps.', 1, 8.0, 1000.00, 'SKTREE_2')
INSERT INTO Skill VALUES ('SKL_1007', 'Hop', 'Can jump slighly higher.', 2, 8.5, 2500.00, 'SKTREE_2')
INSERT INTO Skill VALUES ('SKL_1008', 'High Jump', 'Can jump much higher.', 3, 9.0, 5000.00, 'SKTREE_2')
INSERT INTO Skill VALUES ('SKL_1009', 'Double Jump', 'Can jump extremely higher.', 4, 9.5, 7500.00, 'SKTREE_2')
INSERT INTO Skill VALUES ('SKL_1010', 'Triple Jump', 'Can jump almost as high as the level allows.', 5, 10.0, 10000.00, 'SKTREE_2')

-- first 5 attributes for stamina
INSERT INTO Skill VALUES ('SKL_1011', 'Start Stamina', 'The starting strength for stamina.', 1, 8.0, 1000.00, 'SKTREE_3')
INSERT INTO Skill VALUES ('SKL_1012', 'Stamina 2x', 'When picking up a healthy item, the speed bonus lasts longer.', 2, 5.0, 2500.00, 'SKTREE_3')
INSERT INTO Skill VALUES ('SKL_1013', 'Stamina 3x', 'When picking up a healthy item, the speed bonus is stronger.', 3, 10.0, 5000.00, 'SKTREE_3')
INSERT INTO Skill VALUES ('SKL_1014', 'Stamina 4x', 'When picking up an unhealthy item, the crash effect is 2x shorter.', 4, 3.0, 7500.00, 'SKTREE_3')
INSERT INTO Skill VALUES ('SKL_1015', 'Stamina 5x', 'When picking up an unhealthy item, the crash effect is 2x faster.', 5, 4.0, 10000.00, 'SKTREE_3')


SELECT * FROM Skill


/* ------------------- SKILL TREE BRANCH PROGRESS --------------------*/
CREATE TABLE SkillTreeBranchProgress (
	branchProgressID_ID		CHAR(8)	 PRIMARY KEY,
	overallLevelInBranch	INT		 NOT NULL,
	gameAccount_ID			CHAR(8)	 NOT NULL,
	skillTreeBranch_ID		CHAR(8)	 NOT NULL,
	FOREIGN KEY (gameAccount_ID) REFERENCES GameAccountState(gameAccount_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION,
	FOREIGN KEY (skillTreeBranch_ID) REFERENCES SkillTreeBranch(skillTreeBranch_ID)
		ON UPDATE CASCADE ON DELETE NO ACTION
)

select * from SkillTreeBranchProgress

INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1001', 5, 'ACC_1001', 'SKTREE_1');
INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1002', 2, 'ACC_1001', 'SKTREE_2');
INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1003', 3, 'ACC_1001', 'SKTREE_3');

INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1004', 2, 'ACC_1002', 'SKTREE_1');
INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1005', 5, 'ACC_1002', 'SKTREE_2');
INSERT INTO SkillTreeBranchProgress VALUES ('BRC_1006', 4, 'ACC_1002', 'SKTREE_3');

-- SELECT * FROM SkillTreeBranchProgress


-- selects the fastest time for a user, on a certain level
SELECT levelNumber, fastestTime FROM Level WHERE gameAccount_ID = 
	(SELECT gameAccount_ID FROM GameAccountState WHERE user_ID = 'USR_1001') 
	AND levelNumber = 2
-- instead of USR_1001 put the game account number of the person who is logged in


-- selects all fastest times of a level (can be for leaderboard)
SELECT username, levelNumber, fastestTime FROM Level 
	JOIN GameAccountState ON Level.gameAccount_ID = GameAccountState.gameAccount_ID
	JOIN Login ON Login.user_ID = GameAccountState.user_ID
	WHERE levelNumber = 2 -- Put whatever level number the leaderboard is for
	
-- prints the daily step count of the user
SELECT dailyStepCount FROM DailyStepStats JOIN GameUser ON GameUser.user_ID = DailyStepStats.user_ID
	WHERE date = CAST(GETDATE() AS DATE) 
	AND DailyStepStats.user_ID = 'USR_1001' -- Put the user you want to see the steps of
	
-- Prints the highest acquired level of the speed/jump/stamina tree for a user
SELECT gameAccount_ID, branchName, levelOnBranch as highestSKillOnBranch, skillName, skillStrengthLevel FROM Skill 
	JOIN SkillTreeBranch ON SkillTreeBranch.skillTreeBranch_ID = Skill.skillTreeBranch_ID
	JOIN SkillTreeBranchProgress ON Skill.skillTreeBranch_ID = SkillTreeBranchProgress.skillTreeBranch_ID
	WHERE overallLevelInBranch = levelOnBranch
	AND gameAccount_ID = 'ACC_1001'

	
