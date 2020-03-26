(function () {


    Vue.component('i-grid', {
        template: '  <Layout class="el-grid" v-loading="loading"> \
			            <i-content>\
                            <i-table v-bind:data="tableData" stripe v-bind:height="tableHeight" v-bind:columns="tableColumns">\
                                 <template slot-scope="{ row, index }" slot="action">\
                                </template>\
                            </i-table>\
                        </i-content>\
                        <i-footer class="page">\
				          <div style="float: right;">\
                                <Page :total="100" v-bind:page-size="50" v-bind:current="pageIndex" @@on-change="changePage"></Page>\
                          </div>\
			            </i-footer>\
		         </Layout>',
        props: {
            //默认排序
            defaultSort: {
                type: Object,
                required: true
            },
            //默认每页大小
            defaultPageSize: {
                type: Number,
                default: 20
            },
            pageSizes: {
                type: Array,
                default: function () {
                    return [10, 20, 30, 50, 100];
                }
            },
            //数据源URL
            url: {
                type: String,
                required: true
            },
            //过滤条件
            filter: {
                type: Object,
                default: function () {
                    return {};
                }
            },
            rowClassName: {
                type: Function,
                default: function () {
                    return ""
                }
            },
            cellClassName: {
                type: Function,
                default: function () {
                    return ""
                }
            }
        },
        data: function () {
            return {
                currentPage: 1,
                pageSize: this.defaultPageSize,
                totalRecords: 0,
                tableData: [],
                sort: JSON.parse(JSON.stringify(this.defaultSort)),
                loading: false
            };
        },
        computed: {
            computedPageSize: function () {
                if (this.pageSizes.indexOf(this.defaultPageSize) == -1) {
                    this.pageSizes.push(this.defaultPageSize);
                    this.pageSizes = this.pageSizes.sort(function (a, b) { return a - b; });
                }
                return this.pageSizes;
            }
        },
        methods: {
            //加载数据
            reload: function (pageInfo) {
                var actualPageInfo = this.buildPageInfoSmartly(pageInfo);
                this.currentPage = actualPageInfo.PageNumber;
                this.pageSize = actualPageInfo.PageSize;
                this.loadData(actualPageInfo);
            },
            buildPageInfoSmartly: function (pageInfo) {
                var retPageInfo = null;
                if (!pageInfo) {
                    retPageInfo = {
                        PageSize: this.pageSize,
                        PageNumber: 1,
                        Sort: this.sort
                    };
                }
                else {
                    if (pageInfo.PageNumber == null) { pageInfo.PageNumber = this.currentPage; };
                    if (pageInfo.PageSize == null) { pageInfo.PageSize = this.pageSize; };
                    if (pageInfo.Sort == null) {
                        pageInfo.Sort = this.sort;
                    };
                    retPageInfo = pageInfo;
                }
                return retPageInfo;
            },
            onSortChange: function (column) {
                this.sort.prop = column.prop;
                this.sort.order = column.order;
                this.reload({});
            },
            loadData: function (pageInfo) {
                var self = this;
                if (pageInfo.Sort.prop) {
                    pageInfo.SortExpression = pageInfo.Sort.prop + ' ' + (pageInfo.Sort.order == 'descending' ? 'desc' : 'asc');
                    delete pageInfo.Sort;
                }
                else {
                    pageInfo.SortExpression = this.defaultSort.prop ? (this.defaultSort.prop + ' ' + (this.defaultSort.order == 'descending' ? 'desc' : 'asc')) : '';
                }
                var data = { Filter: self.filter, PageInfo: pageInfo, _t: Math.random() };
                $.ajax({
                    url: this.url,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    beforeSend: function () {
                        self.loading = true;
                    },
                    complete: function () {
                        self.loading = false;
                    }
                }).done(function (result) {
                    self.totalRecords = result.TotalRecords || 0;
                    self.tableData = result.Data || [];
                    self.$emit('load', result);
                });
            },
            getSelection: function () {
                return this.$refs.myTable.selection;
            },
            getColumns: function () {
                return this.$refs.myTable.columns;
            },
            rowClick: function (row, event, column) {
                this.$emit('row-click', row, column, event);
            }

        },
        mounted: function () {
            var self = this;
            var column = this.$refs.myTable.columns.find(function (v) { return v.property == self.defaultSort.prop; });
            if (column && column.sortOrders.length == 3) {
                column.sortOrders = ['ascending', 'descending'];
            }
            this.$nextTick(function () {

                self.$refs.myTable.$on('expand-change', function (row, expandedRows) {
                    self.$emit('expand-change', row, expandedRows);
                });

                self.$refs.myPager.$on('current-change', function (currentPage) {
                    self.currentPage = currentPage;
                    self.loadData({
                        PageNumber: self.currentPage,
                        PageSize: self.pageSize,
                        Sort: self.sort
                    });
                });

                self.$refs.myPager.$on('size-change', function (pageSize) {
                    self.pageSize = pageSize;
                    self.currentPage = 1;
                    var pageInfo = {
                        PageNumber: self.currentPage,
                        PageSize: self.pageSize,
                        Sort: self.sort
                    };
                    self.loadData(pageInfo);
                });
            });
        }
    });
})()