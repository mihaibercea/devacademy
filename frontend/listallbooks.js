console.log("List all books");

let booksList = fetch('http://localhost:5077/api/book')
.then(response => response.json())
.then(data => {
    console.log(data);

    
    // "title": "Clean Code",
    // "author": "Robert C. Martin",
    // "ISBN10": "9780132350884",
    // "ISBN13": "978-0132350884",
    // "publishedDate": "01/08/2008",
    // "numberOfPages": 464,
    // "publisher": "Pearson",
    // "reviewScore": 4.38,

    let booksList = data;

    let table = document.getElementById("booksTable");

    for (let i = 0; i < booksList.length; i++) {
        let row = table.insertRow(1);

        let title = row.insertCell(0);
        title.innerHTML = booksList[i].title;

        let author = row.insertCell(1);
        author.innerHTML = booksList[i].author;

        let isbn10 = row.insertCell(2);
        isbn10.innerHTML = booksList[i].ISBN10;

        let isbn13 = row.insertCell(3);
        isbn13.innerHTML = booksList[i].ISBN13;

        let publishedDate = row.insertCell(4);
        publishedDate.innerHTML = booksList[i].publishedDate;

        let numberOfPages = row.insertCell(5);
        numberOfPages.innerHTML = booksList[i].numberOfPages;

        let publisher = row.insertCell(6);
        publisher.innerHTML = booksList[i].publisher;

        let reviewScore = row.insertCell(7);
        reviewScore.innerHTML = booksList[i].reviewScore;

        let deleteCell = row.insertCell(8);
        deleteCell.innerHTML = `<button onclick="deleteBook(${booksList[i].ISBN10})">Delete</button>`;

    }

});

function deleteBook(isbn10){
    fetch(`http://localhost:5077/api/book/${isbn10}`, {
        method: 'DELETE'
    })
    .then(response =>
    {
        if (response.ok){
            alert("Book deleted successfully");
            location.reload();
        }else{
            alert("There was an error deleting the book");
        }
    })
    .then()
    .catch(error => {
        alert("There was an error deleting the book");
    });
}