let queryString = window.location.search;
let split = queryString.split("=");
let bookId = split[1];
console.log(bookId);

let currentBook = fetch(`http://localhost:62509/api/book/${bookId}`)
.then(response => {
    return response.json()
})
.then((data) =>{
    console.log(data);

    document.getElementById("title").innerHTML = data.title;
    document.getElementById("author").innerHTML = data.author;
    document.getElementById("ISBN10").innerHTML = data.ISBN10;
    document.getElementById("ISBN13").innerHTML = data.ISBN13;
    document.getElementById("publishedDate").innerHTML = data.publishedDate;
    document.getElementById("numberOfPages").innerHTML = data.numberOfPages;
    document.getElementById("publisher").innerHTML = data.publisher;
    document.getElementById("reviewScore").innerHTML = data.reviewScore;
})


function clickedOnSubmit(){
    let bodyContent = {
        "bookId": id,
        "reviewScore": document.getElementById("inputReviewScore").value,
        "reviewText": document.getElementById("inputReviewText").value
    }

    fetch('http://localhost:62509/api/book/review',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(bodyContent)
        })
        .then((response) => {
            console.log('Response: ', response);
            return response.json();
        })
        .then((data) => {
            console.log('Data: ', data);
            alert("Book review inserted successfully");
        })
        .catch((error) => {
            console.error('Error: ', error);
            alert("Error inserting book review");
        });
}

