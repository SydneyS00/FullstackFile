<?php

require 'ConnectionSettings.php';

$itemID = $_POST["itemID"]; 
$userID = $_POST["userID"]; 

//Check the connection 
if($conn->connect_error)
{
    die("Connection failed: " . $conn->connect_error); 
}

//First sql
$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'"; 

$result = $conn->query($sql); 

if($result->num_rows > 0)
{
    //store the item price
    $itemPrice = $result->fetch_assoc()["price"]; 

    //second sql (Delete the item)
    $sql2 = "DELETE FROM usersitems WHERE itemID = '" . $itemID . "' AND userID = '" . $userID. "'"; 

    $result2 = $conn->query($sql2); 
    if($result2)
    {
        //it deleted sucessfully 
        $sql3 = "UPDATE 'users' SET 'coins' = coins + '".$itemPrice."' WHERE 'id' = '".$userID."'"; 
    }
    else
    {
        echo "error: could not delete item"; 
    }
    
}
else
{
    echo "0"; 
}

$conn->close(); 


?>