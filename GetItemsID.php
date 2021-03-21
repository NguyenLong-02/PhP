<?php
  require 'ConnectionSetting.php';



    $userid = $_POST["userid"];

    // Create connection

    // Check connection
    if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
    }
    $sql = "SELECT itemid  FROM useritems WHERE userid = '".$userid."'";

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