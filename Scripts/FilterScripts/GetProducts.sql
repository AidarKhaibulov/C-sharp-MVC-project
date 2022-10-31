SELECT  p."Id"
FROM "Product" p
#
where p."Price">=FIRST_VALUE and p."Price"<=SECOND_VALUE ToReplace