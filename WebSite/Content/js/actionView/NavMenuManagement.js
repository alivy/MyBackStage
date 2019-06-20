var testboke;
var currPage = 1; //当前页
var totalCount;
var dataLIst = [];

window.onload = function () {
    /*取到总条数*/
    /*每页显示多少条  10条*/
    var limit = 5;
    //初始化查询
    queryUserRoleList(currPage, limit);

}
///页面按钮权限控制
function btnAuthorityControl() {



}






//查询用户角色列表数据
function queryUserRoleList(currPage, limit) {
    $.ajax({
        url: "/Jurisdiction/QueryNavMenuList",
        type: 'post',
        data: {
            pageSize: limit,
            pageIndex: currPage,
        },
        dataType: 'json',
        success: function (data) {
            /*拿到总数据*/
            totalCount = data.ReturnEntity.TotalRecordCount; //取出来的是数据总量
            dataLIst = data.ReturnEntity.Data; // 将数据放到一个数组里面（dataLIst 还未声明，莫着急）
            createTable();
            loadPageLimit(currPage, limit, totalCount);
        }
    });
}


/*创建的是一个表格，并将数据放进去*/
function createTable() {
    var html = [];
    html.push(' <table class="table table-striped table-bordered templatemo-user-table" style="margin-left: 0;">');
    html.push(' <thead><tr> <th style="text-align: center">全选</th><th>菜单编号</th><th>菜单名称</th><th>父级编号</th><th>菜单级别</th><th>菜单地址</th><th>页面按钮</th></tr></thead><tbody>');
    //<th>操作</th>
    for (var i = 0; i < dataLIst.length; i++) {
        html.push('<tr>');
        html.push('<td  style="text-align: center"><input type="checkbox" name ="ckb"></td>');
        html.push('<td>' + dataLIst[i].MenuId + '</td>');
        html.push('<td>' + dataLIst[i].MenuName + '</td>');
        html.push('<td>' + dataLIst[i].ParentMenId + '</td>');
        html.push('<td>' + dataLIst[i].Level + '</td>');
        html.push('<td>' + dataLIst[i].Url + '</td>');
        html.push('<td><button class="btn btn-primary" data-toggle="modal" data-target="#modal-1" onclick=setupButton(' + dataLIst[i].MenuId + ') > <i class="fa fa-align-center"></i> &nbsp;设置按钮</button>  &nbsp');
        html.push('</tr>');
    }
    html.push('</tbody></table>');
    var mainObj = $('#mainContent');
    mainObj.empty();
    mainObj.html(html.join('MenuId'));
}
///设置按钮
function setupButton(menuId)
{


}



///数据修改弹窗
$("#btnUpdate").click(function () {
    var checkLength = $("input:checkbox[name='ckb']:checked").length;
    if (checkLength == 0) {
        alert("请至少选择一条记录！");
        return;
    }
    if (checkLength != 1) {
        alert("当前操作只允许操作单条数据！");
        return;
    }
    $("#operationType").val(2);
    $('#modal-1').modal();
    var set = false; //读取标志
    //获取数据
    $("input[type='checkbox']").each(function ()//遍历checkbox的选择状态
    {
        if ($(this).prop("checked") && !set)//如果值为checked表明选中了
        {
            $("#myModalLabel").text("修改");
            //$("#modal-1").find(".form-control").val("");
            var tdData = $(this).closest('tr').find('td');//获取td的那一列数据
            var menuId = tdData.eq(1).text();
            var menuName = tdData.eq(2).text();
            var parentMenId = tdData.eq(3).text();
            var level = tdData.eq(4).text();
            var url = tdData.eq(5).text();
            $("#menuId").val(menuId);
            $("#menuName").val(menuName);
            $("#parentMenId").val(parentMenId);
            $("#level").val(level);
            $("#url").val(url);
            set = true;
        }
    });
});



//新增数据
$("#btnAdd").click(function () {
    $("#modal-1").find(".form-control").val("");
    $('#operationType').val(1);
    $('#modal-1').modal();
});

///增改保存按钮
$("#btn_submit").click(function () {
    var datas = $("#modalMenu").serialize();
    //var operation = $('#operationType').val();
    //datas.ExecutiveAction = operation;
    checkproject(datas);
})


//增删改共用接口
function checkproject(data) {
    $.ajax({
        url: "/Jurisdiction/NavMenuExecutive",
        type: 'post',
        data: data,
        dataType: 'json',
        success: function (data) {
            alert(data.Message);
        }
    });
}



/*创建分页数据*/
function loadPageLimit(currPage, limit, totalCount) {
    var totalPage = totalCount / limit;
    if (totalCount % limit !== 0)
        totalPage = totalPage + 1;
    $('#pageLimit').bootstrapPaginator({
        currentPage: currPage,//当前的请求页面。
        totalPages: totalPage,//一共多少页。
        size: "normal",//应该是页眉的大小。
        bootstrapMajorVersion: 3,//bootstrap的版本要求。
        alignment: "right",
        numberOfPages: limit,//一页列出多少数据。
        itemTexts: function (type, page, current) {//如下的代码是将页眉显示的中文显示我们自定义的中文。
            switch (type) {
                case "first": return "首页";
                case "prev": return "上一页";
                case "next": return "下一页";
                case "last": return "末页";
                case "page": return page;
            }
        },
        // 点击事件，用于刷新整个list列表
        onPageClicked: function (event, originalEvent, type, page) {
            currPage = page;
            queryUserRoleList(page, limit);
        }
    });
}
