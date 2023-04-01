update Books
set Price = 1 + floor(59*rand(Id*1009))
+
case floor(4*rand(Id * 343))
	when 0 then 0.45
	when 1 then 0.49
	when 2 then 0.95
	when 3 then 0.99
end
 