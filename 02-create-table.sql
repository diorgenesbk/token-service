USE UserRoles;
CREATE TABLE User(
	UserID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UserName VARCHAR(20) NOT NULL,
    Password VARCHAR(30) NOT NULL,
	AccessKey VARCHAR(32) NOT NULL	
)

