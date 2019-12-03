//$('#listinvalid').click(function (e) {
//    console.log(15);
//    let page = 1;
//    let status = this.attributes
//    data = {
//        'id': page,
//        'emailStatus': 'notreviewed'
//    }
//    $.ajax(
//        {
//            type: 'Get',
//            url: '/Email/ListEmails',
//            data: data,
//            success: function (data) {
//                $('#main-container').closest('#html').html(data);
//            }
//        })
//}
//)


function GFG_click(a)
{
    console.log($(`#${a} #${a}`).toggle());
}


 



