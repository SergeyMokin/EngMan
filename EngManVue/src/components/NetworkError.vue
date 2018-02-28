<template>
    <div class = "main-content-center">
        <div class = "error-header">Network error - can not connect to the server</div>
        <div class = "error-content">
            <b>Most likely causes:</b>
            <div class = "error-causes-list">
                <ul>
                    <li>You are not connected to the Internet.</li>
                    <li>The website is encountering problems.</li>
                    <li>There might be a typing error in the address</li>
                </ul>
            </div>
        </div>
    </div>
</template>
<style scoped>
    .error-causes-list{
        padding-left: 35px;
    }
    .error-header{
        height: 50px;
        font-size: 35px;
        border-bottom: solid 1px;
    }
    .error-content{
        padding-top: 5px;
    }
</style>