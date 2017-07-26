//批量内容操作
function formatContentOper(_value, _id, _type)
{
	var _str;
	switch(_type){
		case 'img':
			_str = '<a title="'+formatIsImg(_value)+'" class="ico-isimg'+_value+'"></a>';
			break;
		case 'top':
			_str = '<a title="'+formatIsTop(_value)+'" class="ico-istop'+_value+'"></a>';
			break;
		case 'focus':
			_str = '<a title="'+formatIsFocus(_value)+'" class="ico-isfocus'+_value+'"></a>';
			break;
		case 'head':
			_str = '<a title="'+formatIsHead(_value)+'" class="ico-ishead'+_value+'"></a>';
			break;
		default:
			if(_value==1)
				_str = '<a title="已发布" class="ico-ispass'+_value+'"></a>';
			if(_value==-1)
				_str = '<a title="已删除" class="ico-ispass'+_value+'"></a>';
			if(_value==0)
				_str = '<a title="待审核" class="ico-ispass'+_value+'"></a>';
			break;
	}
	return _str;
}