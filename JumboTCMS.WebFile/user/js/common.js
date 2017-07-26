﻿/*---------------蓝色导航下拉菜单*/
function topnavbarStuHover() {
	var cssRule;
	var newSelector;
	for (var i = 0; i < document.styleSheets.length; i++)
		for (var x = 0; x < document.styleSheets[i].rules.length ; x++)
			{
			cssRule = document.styleSheets[i].rules[x];
			if (cssRule.selectorText.indexOf("LI:hover") != -1)
			{
				 newSelector = cssRule.selectorText.replace(/LI:hover/gi, "LI.iehover");
				document.styleSheets[i].addRule(newSelector , cssRule.style.cssText);
			}
		}
	var getElm = document.getElementById("topnavbar").getElementsByTagName("LI");
	for (var i=0; i<getElm.length; i++) {
		getElm[i].onmouseover=function() {
			this.className+=" iehover";
		}
		getElm[i].onmouseout=function() {
			this.className=this.className.replace(new RegExp(" iehover\\b"), "");
		}
	}
}
function checkAll($checkbox) { 
    if($checkbox==null)$checkbox='selectID';
    if ($("#checkedAll").is(":checked")) { // 全选 
        $("input[name='"+$checkbox+"']").each(function() { 
            $(this).attr("checked", true); 
        }); 
    } else { // 取消全选 
        $("input[name='"+$checkbox+"']").each(function() { 
           $(this).attr("checked", false); 
        }); 
    } 

}
