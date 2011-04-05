var d=new Date();
			var day=d.getDay();
			var h=d.getHours();
			
			var AP,thu;
			// xac dinh thu trong tuan
			switch(day)
			{
				case 0: thu="Chủ Nhật " ; break;
				case 1: thu="Thứ Hai " ; break;
				case 2: thu="Thứ Ba" ; break;
				case 3: thu="Thứ Tư" ; break;
				case 4: thu="Thứ Năm" ; break;
				case 5: thu="Thứ Sáu" ; break;
				default:
				thu="Thứ Bảy";
				break;
			}
			if(h<12)
				AP="AM";
			else
				AP="PM";
				var result=""+thu+", "+d.getDate()+"/"+(d.getMonth()+1)+"/"+d.getFullYear()+" ";	
				
			result+="" + d.getHours() + ":"+d.getMinutes()+ " "+ AP+"";
				document.getElementById('ngay').innerHTML=result;