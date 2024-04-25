console.log("Updating book...");

let queryString = window.location.search;
let split = queryString.split("=");
let isbn10 = split[1];

let currentBook = fetch(`http://localhost:5077/api/book/${isbn10}`)
.then(response => {
    return response.json()
})
.then((data) =>{
    console.log(data);

    document.getElementById("title").value = data.title;
    document.getElementById("author").value = data.author;
    document.getElementById("ISBN10").value = data.ISBN10;
    document.getElementById("ISBN13").value = data.ISBN13;
    document.getElementById("publishedDate").value = data.publishedDate;
    document.getElementById("numberOfPages").value = data.numberOfPages;
    document.getElementById("publisher").value = data.publisher;
    document.getElementById("reviewScore").value = data.reviewScore;
})

function clickedOnSubmit(){
    let valid = true;
    console.log("Clicked on submit");
    if (document.getElementById("title").value == "" 
    || document.getElementById("author").value == ""
    || document.getElementById("ISBN10").value == ""
    || document.getElementById("ISBN13").value == ""
    || document.getElementById("publishedDate").value == ""
    || document.getElementById("numberOfPages").value == ""
    || document.getElementById("publisher").value == ""
    || document.getElementById("reviewScore").value == ""){
        valid = false;
    }

    document.getElementById("titleError").hidden = true;
    document.getElementById("authorError").hidden = true;
    document.getElementById("ISBN10Error").hidden = true;
    document.getElementById("ISBN13Error").hidden = true;
    document.getElementById("publishedDateError").hidden = true;
    document.getElementById("numberOfPagesError").hidden = true;
    document.getElementById("publisherError").hidden = true;
    document.getElementById("reviewScoreError").hidden = true;

    if (document.getElementById("title").value == ""){
        document.getElementById("titleError").hidden = false;
        document.getElementById("titleError").innerHTML = "Please fill in the title field<br/>";
        valid = false;
    }
    
    if (document.getElementById("author").value == ""){
        document.getElementById("authorError").hidden = false;
        document.getElementById("authorError").innerHTML = "Please fill in the author field<br/>";
        valid = false;
    }
    if (document.getElementById("ISBN10").value == ""){
        document.getElementById("ISBN10Error").hidden = false;
        document.getElementById("ISBN10Error").innerHTML = "Please fill in the ISBN10 field<br/>";
        valid = false;
    }
    if (document.getElementById("ISBN13").value == ""){
        document.getElementById("ISBN13Error").hidden = false;
        document.getElementById("ISBN13Error").innerHTML = "Please fill in the ISBN13 field<br/>";
        valid = false;
    }
    if (document.getElementById("publishedDate").value == ""){
        document.getElementById("publishedDateError").hidden = false;
        document.getElementById("publishedDateError").innerHTML = "Please fill in the Published Date field<br/>";
        valid = false;
    }else{
        //check the format, ex: 21/04/2024
        let inputDate = document.getElementById("publishedDate").value;
        
        valid = validateDate(inputDate) && valid;
    }
    if (document.getElementById("numberOfPages").value == ""){
        document.getElementById("numberOfPagesError").hidden = false;
        document.getElementById("numberOfPagesError").innerHTML = "Please fill in the Number Of Pages field<br/>";
    }else{
        let numberofPages = document.getElementById("numberOfPages").value;
        valid = validateNumberOfPages(numberofPages) && valid;

    }
    if (document.getElementById("publisher").value == ""){
        document.getElementById("publisherError").hidden = false;
        document.getElementById("publisherError").innerHTML = "Please fill in the publisher field<br/>";
        valid = false;
    }
    if (document.getElementById("reviewScore").value == ""){
        document.getElementById("reviewScoreError").hidden = false;
        document.getElementById("reviewScoreError").innerHTML = "Please fill in the reviewScore field<br/>";
        valid = false;
    }

    
    if (!valid){
        return;
    }

    let bodyContent = {
        "title": document.getElementById("title").value,
        "author": document.getElementById("author").value,
        "ISBN10": document.getElementById("ISBN10").value,
        "ISBN13": document.getElementById("ISBN13").value,
        "publishedDate": document.getElementById("publishedDate").value,
        "numberOfPages": document.getElementById("numberOfPages").value,
        "publisher": document.getElementById("publisher").value,
        "reviewScore": document.getElementById("reviewScore").value
    }

    fetch('http://localhost:5077/api/book',
    {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(bodyContent)
    })
    .then((response) =>{
        console.log('Response: ', response);
        return response;
    })
    .then((data) =>{
        console.log('Data: ', data);
        alert("Book updated successfully");
        window.location.href = "listallbooks.html";
    })
    .catch((error) =>{
        console.error('Error: ', error);
        alert("Error updating book");
    });
}


function validateDate(inputDate){
        let split = inputDate.split("/");
        if (split.length != 3){
            document.getElementById("publishedDateError").hidden = false;
            document.getElementById("publishedDateError").innerHTML = "Please fill in the Published Date in the dd//MM/yyyy format<br/>";
            return false;
        }else{
            let day = split[0];
            let month = split[1];
            let year = split[2];
            if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1900 || year > 2030){
                document.getElementById("publishedDateError").hidden = false;
                document.getElementById("publishedDateError").innerHTML = "Please fill in the Published Date in the dd//MM/yyyy format<br/>";
                return false;
            }
        }
        return true;
}

function validateNumberOfPages(numberofPages){
    if (isNaN(numberofPages)){
            document.getElementById("numberOfPagesError").hidden = false;
            document.getElementById("numberOfPagesError").innerHTML = "Please fill in the Number Of Pages field with a number<br/>";
            return false;
        }else{
            if (numberofPages < 1){
                document.getElementById("numberOfPagesError").hidden = false;
                document.getElementById("numberOfPagesError").innerHTML = "Please fill in the Number Of Pages field with a number greater than 0<br/>";
                return false;
            }
        }
    return true;
}