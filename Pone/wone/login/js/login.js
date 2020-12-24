var RulesFun = {

    PasswordCheck: function (rule, value, callback) {
        if (value.length < 5) {
            callback(new Error("密码至少应该是6位!"));
            return;
        }
        if (/[a-z]/.test(value) && /[A-Z]/.test(value) && /[0-9]/.test(value)) {
            callback();
        } else {
            callback(new Error("密码至少应该包括字母(大小写),数字!"));
        }
    },
    EmailCheck: function (rule, value, callback) {
        if (/^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/.test(value)) {
            callback();
        } else {
            callback(new Error("请输入正确的Email地址!"));
        }

    }
}

let vue_data = {
    Entitys: {
        LoginInfo: {
            Account: '',
            Password: '',
            RememberMe: false
        },
        LoginRules: {
            Account: [{ required: true, message: '账号不能为空', trigger: 'change' }],
            Password: [{ required: true, message: '密码不能为空', trigger: 'change' }]
        }


    },
    DialogEntity: {
        RegEntity: {
            visible: false,
            title: '注册',
            Entity: {
                Account: '',
                Password: '',
                RePassword: '',
                Email: ''

            },
            RegRules: {
                Account: [{ required: true, message: '账号不能为空', trigger: 'change' }],
                Password: [{ validator: RulesFun.PasswordCheck, trigger: 'change' }],
                Email: [{ validator: RulesFun.EmailCheck, trigger: 'change' }]
            }
        }

    }
}

let vue_methods = {
    onLogin: function () {
        var self = this;
        self.$refs.LoginForm.validate(function (ok) {
            if (!ok)
                return;
            self.$message.success("登录:" + JSON.stringify(self.Entitys.LoginInfo));

        });
    },
    showReg: function () {
        var self = this;
        self.DialogEntity.RegEntity.visible = !0;
        self.$nextTick(function () {
            self.$refs.RegForm.resetFields();
        });


    },
    onReg: function () {
        var self = this;
        self.$refs.RegForm.validate(function (ok) {
            if (!ok)
                return;
            self.$message.success("注册:" + JSON.stringify(self.DialogEntity.RegEntity.Entity));
            self.DialogEntity.RegEntity.visible = !!0;
        });
    }
}
//vue初始化完后执行的方法
let vue_mounted = {


}