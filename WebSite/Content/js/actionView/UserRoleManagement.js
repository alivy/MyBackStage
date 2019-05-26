//<script src="~/Content/js/actionView/UserRoleManagement.js"></script>


//var testboke = {
//    "code": 200,
//    "message": null,
//    "data": {
//        "total": 10,//总条数
//        "size": 5,//分页大小-默认为0
//        "pages": 2,//总页数
//        "current": 1,//当前页数
//        "records": [//author-riverLethe-double-slash-note数据部分
//            {
//                "id": 17,//项目id
//                "userName": "Night夜",//发起人名称
//                "companyName": "康佰裕",//发起人公司名称
//                "ptypeName": "13",//发起项目类别
//                "pask": "13",
//                "pname": "13",
//                "pdesc": "13"
//            },
//            {
//                "id": 16,
//                "userName": "Night夜",
//                "companyName": "康佰裕",
//                "ptypeName": "12",
//                "pask": "12",
//                "pname": "12",
//                "pdesc": "12"
//            },
//            {
//                "id": 15,
//                "userName": "BB机",
//                "companyName": "北京电影",
//                "ptypeName": "11",
//                "pask": "11",
//                "pname": "11",
//                "pdesc": "11"
//            },
//            {
//                "id": 14,
//                "userName": "BB机",
//                "companyName": "北京电影",
//                "ptypeName": "9",
//                "pask": "9",
//                "pname": "9",
//                "pdesc": "9"
//            },
//            {
//                "id": 13,
//                "userName": "BB机",
//                "companyName": "北京电影",
//                "ptypeName": "7",
//                "pask": "7",
//                "pname": "7",
//                "pdesc": "7"
//            },
//            {
//                "id": 12,
//                "userName": "Night夜",
//                "companyName": "康佰裕",
//                "ptypeName": "6",
//                "pask": "6",
//                "pname": "6",
//                "pdesc": "6"
//            },
//            {
//                "id": 11,
//                "userName": "BB机",
//                "companyName": "北京电影",
//                "ptypeName": "5",
//                "pask": "5",
//                "pname": "5",
//                "pdesc": "5"
//            },
//            {
//                "id": 10,
//                "userName": "Night夜",
//                "companyName": "康佰裕",
//                "ptypeName": "4",
//                "pask": "4",
//                "pname": "4",
//                "pdesc": "4"
//            },
//            {
//                "id": 9,
//                "userName": "BB机",
//                "companyName": "北京电影",
//                "ptypeName": "3",
//                "pask": "3",
//                "pname": "3",
//                "pdesc": "3"
//            },
//            {
//                "id": 8,
//                "userName": "Night夜",
//                "companyName": "康佰裕",
//                "ptypeName": "2",
//                "pask": "2",
//                "pname": "2",
//                "pdesc": "2"
//            }
//        ]
//    }
//}


var testboke;
var currPage = 1; //当前页
var totalCount;
var dataLIst = [];
window.onload = function () {
    /*取到总条数*/
    /*每页显示多少条  10条*/
    var limit = 5;
    data(currPage, limit)
    //createTable(currPage, limit, totalCount);
    loadPageLimit(currPage, limit, totalCount);
}

/*将数据取出来*/
function data(currPage, limit) {
    queryUserRoleList(currPage, limit);
}

//查询用户角色列表数据
function queryUserRoleList(currPage, limit)
{
    $.ajax({
        url: "/Jurisdiction/QueryUserRoleList",
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
        html.push('<td>' + dataLIst[i].RoleId + '</td>');
        html.push('<td>' + dataLIst[i].RoleName + '</td>');
        html.push('<td>' + dataLIst[i].RoleSeq + '</td>');
        html.push('<td>' + dataLIst[i].CrateUser + '</td>');
        html.push('<td>' + dataLIst[i].CreateDate + '</td>');
        html.push('<td><a href="project_details_init.html?id=' + dataLIst[i].RoleId + '" class="templatemo-edit-btn">详情</a></td>');
        html.push('<td><button class="btn btn-primary" onclick=checkproject(' + dataLIst[i].RoleId + ',"1")>同意</button>  &nbsp');
        html.push('<button class="btn btn-warning" onclick= checkproject(' + dataLIst[i].RoleId + ', "2") > 修改</button > &nbsp');
        html.push('<button class="btn btn-danger" onclick= checkproject(' + dataLIst[i].RoleId + ', "2") > 删除</button ></td > ');
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
    if (totalCount % limit != 0)
        totalPage + 1;
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
            createTable(page, limit, totalCount);
        }
    });
}
