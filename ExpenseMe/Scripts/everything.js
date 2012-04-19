var server = localStorage.server == null ? "" : localStorage.server ;

$(function () {
    $("#addexpense").click(function () {
        if (online) {
            PushExpense(0, $("#expense_description").val(), $("#expense_spent").val());
        } else {
            PushExpenseOffline($("#expense_description").val(), $("#expense_spent").val());
        }
    });
    if ($("#settings_server").length > 0) {
        server = localStorage.server;
        $("#settings_server").change(function () {
            server = $(this).val();
            localStorage.server = server;
        });
        $("#settings_server").val(server);
    }
    UpdateMyExpenseList();
    UpdateListOffline();
});

var online=false;
setInterval(function () {
    if (navigator.onLine) {
        online = true;
        PushExpenseBatch();
        $("#app_status").removeClass("red").addClass("green").html("You are online");
    }
    if (!navigator.onLine) {
        online = false;
        $("#app_status").removeClass("green").addClass("red").html("You are offline");
    }
}, 250);

function PushExpenseOffline(description, spent) {
    if (!localStorage.expenses) {
        localStorage.expenses = JSON.stringify([]);
    }
    // JSON.parse(JSON.stringify(hey))
    var expenses = JSON.parse(localStorage.expenses);
    var now = new Date();
    var nowstring = now.getMonth() + "/" + now.getDay() + "/" + now.getYear() + " " + now.getHours() + ":" + now.getMinutes();
    expenses.push({ Description: description, ExpenseDate: now, FormattedExpenseDate: nowstring, Spent: spent });
    alert("Expense saved locally");
    localStorage.expenses = JSON.stringify(expenses);
    UpdateListOffline();
}

function UpdateListOffline() {
    if (!localStorage.expenses) {
        localStorage.expenses = JSON.stringify([]);
    }
    var expenses = JSON.parse(localStorage.expenses);
    var myoffline = $("#expenses_offline");
    UpdateListMeta(expenses, myoffline);
}

function PushExpense(expenseid, description, spent) {
    var expense;
    //expense = "{ 'expense':'" + expense + "', 'description':'" + description + "', 'spent':'" + spent + "'}";
    expense = { ExpenseId: expenseid, Description: description, ExpenseDate: new Date(), Spent: parseFloat(spent) };
    $.ajax({
        url: server + '/Expense/Create',
        type: 'POST',
        data: JSON.stringify(expense),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert(data.StatusDescription);
            UpdateMyExpenseList();
        },
        error: function (data) {
            alert("Oops! Can't do this now");
        }
    });
    return false;
}

function PushExpenseBatch() {
    if (!localStorage.expenses) {
        localStorage.expenses = JSON.stringify([]);
    }
    var expenses = JSON.parse(localStorage.expenses);
    if (expenses.length > 0) {
        $.ajax({
            url: server + '/Expense/CreateBatch',
            type: 'POST',
            data: JSON.stringify(expenses),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert(data.StatusDescription);
                localStorage.expenses = JSON.stringify([]);
                UpdateMyExpenseList();
                UpdateListOffline();
            },
            error: function (data) {
                alert("Oops! Can't do this now");
            }
        });
    }
    return false;
}

function UpdateMyExpenseList() {
    if (navigator.onLine) {
        $.ajax({
            url: server + '/Expense/Index',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var myexpenses = $("#expenses_myexpenses");
                UpdateListMeta(data, myexpenses);
            },
            error: function (data) {
                alert("Oops! Can't do this now");
            }
        });
    }
}

function UpdateListMeta(data,list) {
    list.children().remove();
    if (data.length > 0) {
        $(data).each(function () {
            var description = $("<span/>").addClass("description").html(" in " + this.Description);
            var price = $("<span/>").addClass("price").html("Spent " + this.Spent);
            //var dateval = Date(this.FormattedExpenseDate.substring(6));
            var dateval = this.FormattedExpenseDate;
            var date = $("<span/>").addClass("date").html(" on " + dateval);
            var li = $("<li/>");
            price.appendTo(li);
            description.appendTo(li);
            date.appendTo(li);
            li.appendTo(list);
        });
    } else {
        var description = $("<span/>").addClass("description").html("No expenses yey!");
        var li = $("<li/>");
        description.appendTo(li);
        li.appendTo(list);
    }
}




