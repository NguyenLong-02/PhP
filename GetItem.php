<?php
  require 'ConnectionSetting.php';


    $itemid = $_POST["itemid"];

    // Create connection

    // Check connection
    if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
    }

    $sql = "SELECT name, description, price FROM items WHERE id = '".$itemid."'"  ;

    $result = $conn->query($sql);
    
    if ($result->num_rows > 0) {
      
        $rows = array();
      while($row = $result->fetch_assoc()) {
        $rows[] = $row;
      }
      echo json_encode($rows);
    } else {
      echo "0 results";
    }
    $conn->close();

?>