var LocalStorage =
{
    SaveData: function (name, data) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem(name, JSON.stringify(data));
        } else {
            console.log("Browser not support Storage");
        }
    },
    GetData: function (name) {
        return JSON.parse(localStorage.getItem(name));
    },
    RemoveData: function(name) {
        if (typeof (Storage) !== "undefined") {
            localStorage.removeItem(name);
        } else {
            console.log("Browser not support Storage");
        }
    },
    ClearAllData: function() {
        localStorage.clear();
    },
    PushMessage: function (id, name, time, message) {
        var convert = {};
        convert.Name = name;
        convert.Time = time;
        convert.Content = message;
        var data = LocalStorage.GetData(id);
        if (data === null || data === "undefined" || data === undefined) {
            var msg = [];
            msg.push(convert);
            LocalStorage.SaveData(id, msg);
        } else {
            data.push(convert);
            LocalStorage.SaveData(id, data);
        }
    },
    PushHtml: function (id, html) {
        try {
            var data = LocalStorage.GetData(id);
            if (data === null || data === "undefined" || data === undefined) {
                var msg = [];
                msg.push(html);
                LocalStorage.SaveData(id, msg);
            } else {
                data.push(html);
                LocalStorage.SaveData(id, data);
            }
        } catch (e) {
            console.log(e);
        }
    }
}

var CookieStorage = {
    
}