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

$sql = "SELECT password, id FROM user WHERE username = '".$loginUser."'"; 

$result = $conn->query($sql); 

if ($result->num_rows > 0)
{
    //output the data of each row
    while($row = $result->fetch_assoc())
    {
        if($row["password"] == $loginPass)
        {
            echo $row["id"]; 
            //Get user data here

            //Get player info

            //Get inventory

            //Modify player data

            //Update inventory
            

        }
        else
        {
            echo "Wrong credentials :( "; 
        }
    }
}
else
{
    echo "Username does not exist"; 
}

$conn->close(); 


?>