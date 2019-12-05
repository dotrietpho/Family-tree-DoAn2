Bước 1: 
  Bật file CayGiaPha.sln:
    Chạy các lệnh tạo database:
      enable-migrations
      add-migration createdb
      update-database
    Ctrl + F5 để start server api (Visual studio)
    
Bước 2:
  Mở folder CayGiaPha bằng trình duyệt hỗ trợ terminal:
    Start angular project ng-serve --o

Để chạy được chương trình yêu cầu: 
      Có dữ liệu mẫu của node cha đầu tiên (Thêm bằng POST API thông qua cổng localhost/api/nodes/)
      Sữa trong tree.service: hostULR, apiURL, fetchTree
      Chạy những lệnh trigger trong sql 
    
  
  
