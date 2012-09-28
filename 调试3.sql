declare @tablename char(100)
set @tablename='MyUser'

--找一个表的所有列相关信息
select thecol.name ColName, thetable.name TableName, thetype.name ColType, thecol.length,
	   COLUMNPROPERTY(thecol.id, thecol.name, 'IsIdentity') IsIdentity,
	   CASE WHEN EXISTS
          (SELECT 1
          FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
          WHERE sc.id = thecol.id AND sc.colid = thecol.colid) THEN 'y' ELSE 'n' END AS IsPrime
from syscolumns thecol inner join
	 sysobjects thetable on thecol.id=thetable.id left outer join
	 systypes thetype on thecol.xusertype=thetype.xusertype
where thetable.type='U' and thetable.name=@tablename

--select * from sysobjects