USE PiggyBank;

INSERT INTO Item(Name, Price) VALUES('Szczypior', 9.99);
INSERT INTO Item(Name, Price, ExpenseId) VALUES('Papier toaletowy', 12.99, 1);
INSERT INTO Item(Name, Price, ExpenseId) VALUES('Pomidor', 3.99, 2);
INSERT INTO Item(Name, Price, ExpenseId) VALUES('Marchewka', 4.50, 2);
INSERT INTO Item(Name, Price, ExpenseId) VALUES('Lodowka', 399.99, 3);
INSERT INTO Item(Name, Price, ExpenseId) VALUES('Item', 5.99, 3);

INSERT INTO Expense(Name, PurchaseDate, RoomId) VALUES('Papier toaletowy', '05/06/2024', 1);
INSERT INTO Expense(Name, PurchaseDate, RoomId) VALUES('Zakupy', '06/06/2024', 2);
INSERT INTO Expense(Name, PurchaseDate, RoomId) VALUES('AGD', '06/06/2024', 2);

INSERT INTO Room(Name) VALUES('Marek_Sienkiewicza_10A');
INSERT INTO Room(Name) VALUES('Jarek_Mandarynkowa_37');
INSERT INTO Room(Name, Password) VALUES('Lucek_Chrzanowa_07', 'hotdog123');