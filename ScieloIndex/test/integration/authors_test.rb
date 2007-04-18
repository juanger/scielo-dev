require "#{File.dirname(__FILE__)}/../test_helper"

class AuthorsTest < ActionController::IntegrationTest
  fixtures :authors
  def setup
    @authors = [:hector, :memo, :mono]
  end

   def test_getting_index
     get "/authors"
     assert_equal 200, status
     assert_equal '/authors', path
   end

   def test_new
     get "/authors/new"
     assert_equal 200, status
     assert_equal '/authors/new', path
   end

   
   def  test_creating_new_author
     post "authors/create", :author =>  {:id => 100, :firstname => 'Fernando', :lastname => 'Maldonado'}
     assert_equal 302, status
     follow_redirect!
     assert_equal '/authors/list', path
   end

   def test_showing
     @authors.each { | author |
       post "/authors/show", :id => authors(author).id
       assert 200, status
       assert_equal '/authors/show', path
     }
   end

   def test_editing
     @authors.each { | author |
       post "/authors/edit", :id => authors(author).id
       assert 200, status
       assert "/authors/edit/#{authors(author).id}", path
     }
   end

   def test_updating_name
     @authors.each { | author |
       post "/authors/update", :id => authors(author).id, :firstname => authors(author).firstname.reverse, :lastname => authors(author).lastname.reverse 
       assert 302, status
       follow_redirect!
       assert "/authors/show/#{authors(author).id}", path
     }
   end

    def test_deleting
     @authors.each { | author |
       get "/authors/destroy", :id => authors(author).id
       assert 302, status
       assert '/authors/list',  path
     }
   end
end