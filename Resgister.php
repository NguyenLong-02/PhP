<?php
  require 'ConnectionSetting.php';
    

    // variables by user

    $loginUser = $_POST["loginUser"];
    $loginPass = $_POST["loginPass"];
    $loginPassCon = $_POST["loginPassConf"];



    // Create connection


    // Check connection
    if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT username FROM users WHERE username = '".$loginUser."'";

    $result = $conn->query($sql);
    
    if ($result->num_rows > 0) {
        echo "Username is already exist";    
    } else {
        echo "Creating user <br>";
        if($loginPass == $loginPassCon)
        {
            $sql_2 = "INSERT INTO users (username, password,level, coins) VALUES ('".$loginUser."','".$loginPass."', 2, 10)";
            if ($conn->query($sql_2) === TRUE) {
                echo "New record created successfully";
            } else {
                echo "Error: " . $sql_2 . "<br>" . $conn->error;
            }
        }else
        {
            echo "You Enter Wrong Or Missing Password Confirm";
        }

    }
    $conn->close();

?>