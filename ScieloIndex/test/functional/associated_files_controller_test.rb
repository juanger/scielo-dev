require File.dirname(__FILE__) + '/../test_helper'
require 'associated_files_controller'

# Re-raise errors caught by the controller.
class AssociatedFilesController; def rescue_action(e) raise e end; end

class AssociatedFilesControllerTest < Test::Unit::TestCase
  fixtures :articles, :associated_files

  def setup
    @controller = AssociatedFilesController.new
    @request    = ActionController::TestRequest.new
    @response   = ActionController::TestResponse.new

    @first_id = associated_files(:art4files).id
    @pdf = Tempfile.new("pdf")
    @pdf.write(File.open("#{RAILS_ROOT}/test/files/v17n01a01.pdf", 'r').read())
    @pdf.open()
    @html = Tempfile.new("html")
    @html.write(File.open("#{RAILS_ROOT}/test/files/v17n01a01.htm", 'r').read())
    @html.open()
    @myassociated_file = {:article_id => 3, :filename => 'v17n01a01', :pdfdata => @pdf, :htmldata => @html}
  end

  def test_index
    get :index
    assert_response :success
    assert_template 'list'
  end

  def test_list
    get :list

    assert_response :success
    assert_template 'list'

    assert_not_nil assigns(:collection)
  end

  def test_show
    get :show, :id => @first_id

    assert_response :success
    assert_template 'show'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_new
    get :new

    assert_response :success
    assert_template 'new'

    assert_not_nil assigns(:record)
  end

  def test_create
    num_associated_files = AssociatedFile.count

    post :create, :record => @myassociated_file

    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_equal num_associated_files + 1, AssociatedFile.count
  end

  def test_edit
    get :edit, :id => @first_id

    assert_response :success
    assert_template 'edit'

    assert_not_nil assigns(:record)
    assert assigns(:record).valid?
  end

  def test_update
    post :update, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'show', :id => @first_id
  end

  def test_destroy
    assert_nothing_raised {
      AssociatedFile.find(@first_id)
    }

    Dir.mkdir("#{RAILS_ROOT}/public/associated_files/9997")

    post :destroy, :id => @first_id
    assert_response :redirect
    assert_redirected_to :action => 'list'

    assert_raise(ActiveRecord::RecordNotFound) {
      AssociatedFile.find(@first_id)
    }
  end

  def teardown
    dir = "#{RAILS_ROOT}/public/associated_files/3"
    if File.exists?(dir)
      Dir.foreach(dir) {|file|
        next if file =~ /^\.\.?$/
        file = File.join(dir, file)
        File.delete(file)
      }
      Dir.delete(dir)
    end
  end
end
