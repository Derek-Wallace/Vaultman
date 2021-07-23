CREATE TABLE keeps(  
    id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
    name varchar(255),
    description varchar(255),
    img VARCHAR(255),
    views int DEFAULT 0,
    shares int DEFAULT 0,
    keeps int DEFAULT 0,
    creatorId varchar(255) comment 'FK: Account Id',
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 comment '';
CREATE TABLE vaults(  
    id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'create time',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'update time',
    name varchar(255),
    description varchar(255),
    img VARCHAR(255),
    views int DEFAULT 0,
    isPrivate TINYINT DEFAULT 0,
    creatorId varchar(255) comment 'FK: Account Id',
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 comment '';
