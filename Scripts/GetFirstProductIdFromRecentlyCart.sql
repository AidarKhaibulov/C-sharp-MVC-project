SELECT  p."Id"
FROM "Product" p
         JOIN "ProductRecentlyWatchedRelation" rel ON p."Id" = rel."ProductId"
         JOIN "RecentlyWatchedCart" f ON f."Id" = rel."RecentlyWatchedCartId"
         JOIN "User" u ON u."Id" = f."UserId"
WHERE u."Id" = ToReplace
    fetch first 1 rows only;
