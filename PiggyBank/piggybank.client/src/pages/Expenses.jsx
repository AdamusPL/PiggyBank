import { useEffect, useState, useRef } from 'react';
import '../css/Expenses.css';
import { React } from 'react';

export function Expenses() {
    const [userRooms, setUserRooms] = useState([]);
    const [itemName, setItemName] = useState('');
    const [itemPrice, setItemPrice] = useState('');
    const [expenseName, setExpenseName] = useState('');
    const [purchaseDate, setPurchaseDate] = useState('');

    useEffect(() => {
        populateUserExpensesData();
    }, []);

    async function populateUserExpensesData() {
        const userId = JSON.parse(localStorage.getItem('user')).id;
        const response = await fetch(`items/GetRoomExpenses?userId=${userId}`);
        const data = await response.json();

        setUserRooms(data);
        console.log(data);
    }

    async function handleSubmitItem(roomId, expenseId, item) {
        try {
            const itemToAdd = {
                Name: item.name,
                Price: item.price,
                ExpenseId: expenseId
            };

            const response = await fetch('items/AddItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(itemToAdd),
            });

            const result = await response.json();
        } catch (error) {
            console.error('Error:', error);
        }
    };

    function handleFormItemSubmit(roomId, expenseId) {
        const item = {
            name: itemName,
            price: itemPrice
        };
        handleSubmitItem(roomId, expenseId, item);
    };

    async function handleSubmitExpense(roomId, expense) {
        try {
            const expenseToAdd = {
                Name: expense.name,
                PurchaseDate: expense.purchaseDate,
                RoomId: roomId
            };

            const response = await fetch('items/AddExpense', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(expenseToAdd),
            });

        } catch (error) {
            console.error('Error:', error);
        }
    };

    function handleFormExpenseSubmit(roomId) {
        debugger;
        const expense = {
            name: expenseName,
            purchaseDate: purchaseDate
        };
        handleSubmitExpense(roomId, expense);
    };

    async function removeItem(roomId, expenseId, itemId, price) {
        try {
            const response = await fetch(`items/RemoveItem?itemId=${itemId}`, {
                method: 'POST'
            });

            console.log(userRooms);

            setUserRooms((prevRooms) =>
                prevRooms.map((room) => {
                    if (room.id === roomId) {
                        return {
                            ...room,
                            sumExpenses: Math.round(room.sumExpenses - price, 2),
                            expenses:
                                room.expenses.map((expense) => {
                                    if (expense.id === expenseId) {
                                        return {
                                            ...expense,
                                            sumItems: Math.round(expense.sumItems - price, 2),
                                            items: expense.items.filter((item) => item.id !== itemId)
                                        };
                                    }
                                    return expense;
                                })
                        };
                    }
                    return room;
                })
            )


        } catch (error) {
            console.error('Error:', error);
        }
    }

    async function removeExpense(roomId, expenseId, sumItems) {
        try {
            const response = await fetch(`items/RemoveExpense?expenseId=${expenseId}`, {
                method: 'POST'
            });

            setUserRooms((prevRooms) =>
                prevRooms.map((room) => {
                    if (room.id === roomId) {
                        return {
                            ...room,
                            sumExpenses: Math.round(room.sumExpenses - sumItems, 2),
                            expenses: room.expenses.filter((expense) => expense.id !== expenseId)
                        };
                    }
                    return room;
                })
            );

        } catch (error) {
            console.error('Error:', error);
        }
    }

    return (
        <div>
            <h1 id="tableLabel">Expenses</h1>
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Room name</th>
                    </tr>
                </thead>
                <tbody>
                    {userRooms.length > 0 ?
                        userRooms.map(room => (
                            <div className="h-100 p-5 bg-body-tertiary border rounded-3" key={room.id}>
                                <h2>{room.name}</h2>
                                {room.expenses.length > 0 ?
                                    room.expenses.map(expense => (
                                        <div className="h-100 p-5 bg-body-tertiary border rounded-3" key={expense.id}>
                                            <div className="one-row">
                                                <h3>{expense.name}</h3>
                                                <button className="btn btn-outline-secondary" type="button" onClick={() => removeExpense(room.id, expense.id, expense.sumItems)}>X</button>
                                            </div>
                                            {expense.items.length > 0 ?
                                                expense.items.map(item => (
                                                    <div className="one-row" key={item.id}>
                                                        <p>{item.name}: {item.price}</p>
                                                        <button className="btn btn-outline-secondary" type="button" onClick={() => removeItem(room.id, expense.id, item.id, item.price)}>X</button>
                                                    </div>
                                                ))
                                                : <p>No items</p>
                                            }

                                            <h3>Add new item</h3>
                                            <div className="new-items">
                                                <form className="item-form" onSubmit={() => handleFormItemSubmit(room.id, expense.id)}>
                                                    <p>Name</p>
                                                    <input type="search" onChange={(e) => setItemName(e.target.value)} />
                                                    <p>Price</p>
                                                    <input type="search" onChange={(e) => setItemPrice(e.target.value)} />
                                                    <button className="btn btn-outline-secondary" type="submit">Add</button>
                                                </form>
                                            </div>

                                            <p>Summary of expense: {expense.sumItems}</p>

                                        </div>
                                    ))
                                    :
                                    <p>No expenses</p>
                                }

                                <p>Summary of room expenses: {room.sumExpenses}</p>

                                <div className="new-expenses">
                                    <h3>Add new expense</h3>
                                    <form className="expense-form" onSubmit={() => handleFormExpenseSubmit(room.id)}>
                                        <p>Name</p>
                                        <input type="search" onChange={(e) => setExpenseName(e.target.value)} />
                                        <p>Purchase Date</p>
                                        <input type="search" onChange={(e) => setPurchaseDate(e.target.value)} />
                                        <button className="btn btn-outline-secondary" type="submit">Add</button>
                                    </form>
                                </div>
                            </div>
                        ))
                        : <p>You haven't joined any room yet</p>}
                </tbody>
            </table>
        </div>
    );
}

export default Expenses;
