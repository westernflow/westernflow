package controller

import (
	"net/http"
	_ "uwo-tt-api/httputil"
	_ "uwo-tt-api/model"
)

// // ShowAccount godoc
// // @Summary Show an account
// // @Description get string by ID
// // @Tags accounts
// // @Accept  json
// // @Produce  json
// // @Param id path int true "Account ID"
// // @Success 200 {object} model.Account
// // @Failure 400 {object} httputil.HTTPError
// // @Failure 404 {object} httputil.HTTPError
// // @Failure 500 {object} httputil.HTTPError
// // @Router /accounts/{id} [get]
// func (c *Controller) ShowAccount(ctx *gin.Context) {
// 	id := ctx.Param("id")
// 	aid, err := strconv.Atoi(id)
// 	if err != nil {
// 		httputil.NewError(ctx, http.StatusBadRequest, err)
// 		return
// 	}
// 	account, err := model.AccountOne(aid)
// 	if err != nil {
// 		httputil.NewError(ctx, http.StatusNotFound, err)
// 		return
// 	}
// 	ctx.JSON(http.StatusOK, account)
// }

// ListCourses Handler for listing courses
func (c *Controller) ListCourses(w http.ResponseWriter, r *http.Request) {
	hitEndpoint("Courses")
}
