SELECT p."Id"
FROM "Product" p
         JOIN "ProductRecentlyWatchedRelation" rel ON p."Id" = rel."ProductId"
         JOIN "RecentlyWatchedCart" r ON r."Id" = rel."RecentlyWatchedCartId"
         JOIN "User" u ON u."Id" = r."UserId"
WHERE u."Id" = ToReplace;
