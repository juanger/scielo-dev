require "#{File.dirname(__FILE__)}/../test_helper"

class AuthorInstitutionsTest < ActionController::IntegrationTest
  fixtures :author_institutions

  def setup
    @author_institutions = [:monoipn, :hectorunam]
  end

   def test_getting_index
     get "/author_institutions"
     assert_equal 200, status
     assert_equal '/author_institutions', path
   end

   def test_new
     get "/author_institutions/new"
     assert_equal 200, status
     assert_equal '/author_institutions/new', path
   end


   def  test_creating_new_author_institutions
     post "author_institutions/create", :record =>  {:id => 3, :author_id => 2, :institution_id => 1}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/author_institutions/list', path
   end

   def test_showing
     @author_institutions.each { | author_institutions |
       post "/author_institutions/show", :id => author_institutions(author_institutions).id
       assert 200, status
       assert_equal '/author_institutions/show', path
     }
   end

   def test_editing
     @author_institutions.each { | author_institutions |
       post "/author_institutions/edit", :id => author_institutions(author_institutions).id
       assert 200, status
       assert "/author_institutions/edit/#{author_institutions(author_institutions).id}", path
     }
   end

   def test_updating_name
     @author_institutions.each { | author_institutions |
       post "/author_institutions/update", :id => author_institutions(author_institutions).id, :author_id => author_institutions(author_institutions).author_id, :institution_id => author_institutions(author_institutions).institution_id
       assert 302, status
       follow_redirect!
       assert "/author_institutions/show/#{author_institutions(author_institutions).id}", path
     }
   end

    def test_deleting
     @author_institutions.each { | author_institutions |
       get "/author_institutions/destroy", :id => author_institutions(author_institutions).id
       assert 302, status
       assert '/author_institutions/list',  path
     }
   end
end
