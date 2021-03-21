<?php
    require 'ConnectionSetting.php';
    // variables by user

    $userid = $_POST["userid"];
    $itemid = $_POST["itemid"];


    

    // Create connection

    // Check connection
    if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT price FROM items WHERE id = '".$itemid."'";

    $result = $conn->query($sql);
    
    if ($result->num_rows > 0)
    {
        $itemPrice = $result->fetch_assoc()["price"];

        $sql_delete = "DELETE FROM useritems WHERE itemid = '".$itemid."' AND userid = '".$userid."'";

        $result_delete = $conn->query($sql_delete);
        if($result_delete)
        {
            $sql_update = "UPDATE `users` SET `coins` = coins + '". $itemPrice."' WHERE `id` = '".$userid."'";
            $conn->query($sql_update);
        }else
        {
            echo "Could not delete items";
        }
    }
    else {
        echo "0";
    }
    $conn->close();

?>