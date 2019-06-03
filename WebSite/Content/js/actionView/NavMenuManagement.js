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
function btnAuthorityControl()
{



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
    html.push(' <thead><tr><th>角色编号</th><th>角色名称</th><th>排序</th><th>创建人</th><th>创建日期</th><th>详情</th><th>操作</th></tr></thead><tbody>');

    for (var i = 0; i < dataLIst.length; i++) {
        html.push('<tr>');
        html.push('<td>' + dataLIst[i].MenuId + '</td>');
        html.push('<td>' + dataLIst[i].MenuName + '</td>');
        html.push('<td>' + dataLIst[i].ParentMenId + '</td>');
        html.push('<td>' + dataLIst[i].Level + '</td>');
        html.push('<td>' + dataLIst[i].Url + '</td>');
        html.push('<td><a href="project_details_init.html?id=' + dataLIst[i].MenuId + '" class="templatemo-edit-btn">详情</a></td>');
        html.push('<td><button class="btn btn-primary" onclick=checkproject(' + dataLIst[i].MenuId + ',"1")>同意</button>  &nbsp');
        html.push('<button class="btn btn-warning" onclick= checkproject(' + dataLIst[i].MenuId + ', "2") > 修改</button > &nbsp');
        html.push('<button class="btn btn-danger" onclick= checkproject(' + dataLIst[i].MenuId + ', "2") > 删除</button ></td > ');
        html.push('</tr>');
    }
    html.push('</tbody></table>');
    var mainObj = $('#mainContent');
    mainObj.empty();
    mainObj.html(html.join(''));
   
}


/*创建分页数据*/
function loadPageLimit(currPage, limit, totalCount) {
    var totalPage = totalCount / limit;
    if (totalCount % limit !== 0)
        totalPage =totalPage + 1;
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
            queryUserRoleList(page, limit);
        }
    });
}
