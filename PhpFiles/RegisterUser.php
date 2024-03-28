<?php
$servername = "localhost"; 
$username = "root"; 
$password = ""; 
$dbname = "unitybackendtutorial"; 

//variables submitted by user
$loginUser = $_POST["loginUser"]; 
$loginPass = $_POST["loginPass"];

//Create a connection 
$conn = new mysqli($servername, $username, $password, $dbname); 

//Check the connection 
if($conn->connect_error)
{
    die("Connection failed: ".$conn->connect_error); 
}

$sql = "SELECT username FROM user WHERE username = '".$loginUser."'"; 

$result = $conn->query($sql); 

if ($result->num_rows > 0)
{
    //Tell user that name is already taken 
    echo "Username is already taken"; 


    
}
else
{
    echo "Create a new user"; 

    //Insert the user and password into data base
    $sql = "INSERT INTO user (username, password, level, coins) VALUES ('".$loginUser."', '".$loginPass."', 1, 0)"; 

    if($conn->query($sql) == TRUE)
    {
        echo "New record created successfully";
    }
    else
    {
        echo "Error: ".$sql."<br>".$conn->error; 
    }
}

$conn->close(); 


?>